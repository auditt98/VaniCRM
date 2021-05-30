using Backend.Domain;
using Backend.Models.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Newtonsoft.Json;
using Backend.Extensions;
using Backend.Resources;

namespace Backend.Repository
{
    public class TaskTemplateRepository
    {
        DatabaseContext db = new DatabaseContext();
        TagRepository _tagRepository = new TagRepository();
        GoogleCalendar _googleCalendar = new GoogleCalendar();
        EmailManager _emailManager = new EmailManager();
        //get user's tasks

        public (List<TASK_TEMPLATE> tasks, Pager p) GetUserTaskTemplate(int userID, string q = "", int currentPage = 1, int pageSize = 0, string status = "")
        {
            var dbUser = db.USERs.Find(userID);
            //var templates = dbUser.TaskTemplateCreated.ToList();
            //var templates = List<TASK_TEMPLATE>();
            var dbStatus = db.TASK_STATUS.Where(c => c.Name.ToLower().Contains(status.ToLower())).FirstOrDefault();

            var templates = db.CALLs.Where(c => c.CallOwner == dbUser.ID || c.TASK_TEMPLATE.CreatedBy == dbUser.ID).Select(c => c.TASK_TEMPLATE).ToList();
            var tasks = db.TASKs.Where(c => c.TaskOwner == dbUser.ID || c.TASK_TEMPLATE.CreatedBy == dbUser.ID).Select(c => c.TASK_TEMPLATE).ToList();
            templates.AddRange(tasks);
            var meetingAsHost = db.MEETINGs.Where(c => c.Host == dbUser.ID).Select(c => c.TASK_TEMPLATE).ToList();
            templates.AddRange(meetingAsHost);

            if (dbStatus != null)
            {
                templates = templates.Where(c => c.TASK_STATUS_ID == dbStatus.ID).ToList();
            }

            foreach (var meeting in dbUser.MEETING_PARTICIPANT)
            {
                templates.Add(meeting.MEETING.TASK_TEMPLATE);
            }

            if (pageSize == 0)
            {
                pageSize = 10;
            }

            if (String.IsNullOrEmpty(q))
            {
                Pager pager = new Pager(templates.Count(), currentPage, pageSize, 9999);
                return (templates.OrderByDescending(c=>c.CreatedAt).OrderByDescending(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(), pager);
            }
            var result = templates.Where(c => c.Title.ToLower().Contains(q.ToLower())).OrderByDescending(c => c.CreatedAt).OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            if (result.Count() > 0)
            {
                Pager p = new Pager(result.Count(), currentPage, pageSize, 9999);

                return (result.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(), p);
            }
            else
            {
                return (result, null);
            }
        }

        public IEnumerable<CALL_RESULT> GetAllCallResults()
        {
            return db.CALL_RESULT;
        }

        public IEnumerable<CALL_REASON> GetAllCallReasons()
        {
            return db.CALL_REASON;
        }

        public IEnumerable<TASK_STATUS> GetAllTaskStatuses()
        {
            return db.TASK_STATUS;
        }

        public IEnumerable<CALL_TYPE> GetAllCallTypes()
        {
            return db.CALL_TYPE;
        }

        public int GetCallTemplateId(int id)
        {
            var dbCall = db.CALLs.Find(id);

            if(dbCall != null)
            {
                return dbCall.TASK_TEMPLATE.ID;
            }
            else { return 0; }
        }

        public bool CreateCall(CallCreateApiModel apiModel, int createdUser)
        {
            //create new template
            var newTemplate = new TASK_TEMPLATE();
            newTemplate.CreatedAt = DateTime.Now;
            newTemplate.ModifiedAt = DateTime.Now;
            newTemplate.CreatedBy = createdUser;
            newTemplate.Description = apiModel.description;
            newTemplate.IsRepeat = apiModel.isReminder;
            newTemplate.RRule = apiModel.rrule;
            newTemplate.Title = apiModel.title;

            var startTime = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.startTime);
            if(startTime != null)
            {
                newTemplate.StartDate = startTime;
                newTemplate.DueDate = startTime.Value.AddSeconds(apiModel.duration);
            }
            if(apiModel.status != 0)
            {
                newTemplate.TASK_STATUS_ID = apiModel.status;

            }
            if(apiModel.priority != 0)
            {
                newTemplate.PRIORITY_ID = apiModel.priority;
            }
            
            db.TASK_TEMPLATE.Add(newTemplate);
            //create call
            var newCall = new CALL();
            newCall.TASK_TEMPLATE = newTemplate;
            if(apiModel.type != 0)
            {
                newCall.CALLTYPE_ID = apiModel.type;
            }
            
            newCall.CallOwner = apiModel.owner != 0 ? apiModel.owner : createdUser;

            if (apiModel.purpose != 0)
            {
                newCall.CALLREASON_ID = apiModel.purpose;

            }

            if(apiModel.result != 0)
            {
                newCall.CALLRESULT_ID = apiModel.result;

            }
            newCall.Length = apiModel.duration;

            if(apiModel.relatedAccount != 0)
            {
                newCall.RELATED_ACCOUNT = apiModel.relatedAccount;
            }
            if(apiModel.relatedCampaign != 0)
            {
                newCall.RELATED_CAMPAIGN = apiModel.relatedCampaign;
            }
            if(apiModel.relatedDeal != 0)
            {
                newCall.RELATED_DEAL = apiModel.relatedDeal;
            }
            if(apiModel.contact != 0)
            {
                newCall.CONTACT_ID = apiModel.contact;
            }
            if (apiModel.lead != 0)
            {
                newCall.LEAD_ID = apiModel.lead;
            }
            
            newCall.StartTime = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.startTime);
            db.CALLs.Add(newCall);
            db.SaveChanges();

            var owner = db.USERs.Find(newCall.CallOwner);
            var creator = db.USERs.Find(createdUser);

            try
            {
                if (owner != null)
                {
                    var extendedProperties = new Dictionary<string, string>();

                    if (apiModel.relatedAccount != 0)
                    {
                        var account = db.ACCOUNTs.Find(apiModel.relatedAccount);
                        if (account != null)
                        {
                            extendedProperties.Add("Account's Email", account.Email);
                            extendedProperties.Add("Account's Name", account.Name);
                            extendedProperties.Add("Account's Phone", account.Phone);
                        }

                    }
                    if (apiModel.relatedCampaign != 0)
                    {
                        var campaign = db.CAMPAIGNs.Find(apiModel.relatedCampaign);
                        if (campaign != null)
                        {
                            extendedProperties.Add("Campaign's Name", campaign.Name);
                        }
                    }
                    if (apiModel.relatedDeal != 0)
                    {
                        var deal = db.DEALs.Find(apiModel.relatedDeal);
                        if (deal != null)
                        {
                            extendedProperties.Add("Deal's Name", deal.Name);
                            extendedProperties.Add("Deal's Amount", deal.Amount.ToString());
                        }
                    }
                    if (apiModel.contact != 0)
                    {
                        var contact = db.CONTACTs.Find(apiModel.contact);
                        if (contact != null)
                        {
                            extendedProperties.Add("Contact's Name", contact.Name);
                            extendedProperties.Add("Contact's Phone", contact.Phone);
                            extendedProperties.Add("Contact's Email", contact.Email);
                            extendedProperties.Add("Contact's Skype", contact.Skype);
                            extendedProperties.Add("Assistant's Name", contact.AssistantName);
                            extendedProperties.Add("Assistant's Phone", contact.AssistantPhone);
                        }

                    }
                    if (apiModel.lead != 0)
                    {
                        var lead = db.LEADs.Find(apiModel.lead);
                        if (lead != null)
                        {
                            extendedProperties.Add("Lead's Name", lead.Name);
                            extendedProperties.Add("Lead's Phone", lead.Phone);
                            extendedProperties.Add("Lead's Email", lead.Email);
                        }

                    }

                    var calendarId = owner.CalendarId;
                    var calEvent = _googleCalendar.AddEvent(endDate: newTemplate.DueDate, startDate: startTime, calendarId: calendarId, description: newTemplate.Description, summary: "[Call] " + newTemplate.Title, rrule: newTemplate.RRule, recur: newTemplate.IsRepeat.Value, extendedProperties: extendedProperties);
                    if (calEvent != null)
                    {
                        newTemplate.EventId = calEvent.Id;
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
            }

            var notifyModel = new NotificationApiModel();
            notifyModel.title = "Call created";
            notifyModel.content = $"Call {newTemplate.Title} has been created and assigned to you by {creator?.Username}.";
            notifyModel.createdAt = DateTime.Now;
            notifyModel.module = "calls";
            notifyModel.moduleObjectId = newCall.ID;
            NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
            return true;
        }

        public int GetCallOwner(int id)
        {
            var dbCall = db.CALLs.Find(id);
            if(dbCall != null)
            {
                return dbCall.Owner.ID;
            }
            else
            {
                return 0;
            }
        }

        public bool AddTagToCall(int id, string tagName)
        {
            var dbCall = db.CALLs.Find(id);
            var dbTag = _tagRepository.GetOneByName(tagName);
            if (dbCall != null)
            {
                if (dbTag != null)
                {
                    var tagItem = dbCall.TAG_ITEM.Where(c => c.TAG_ID == dbTag.ID).FirstOrDefault();
                    if (tagItem == null)
                    {
                        var newTagItem = new TAG_ITEM();
                        newTagItem.TAG_ID = dbTag.ID;
                        newTagItem.CALL_ID = dbCall.ID;
                        db.TAG_ITEM.Add(newTagItem);
                        db.SaveChanges();

                        var owner = db.USERs.Find(dbCall.CallOwner);

                        var notifyModel = new NotificationApiModel();
                        notifyModel.title = "Tag added";
                        notifyModel.content = $"Tag {tagName} has been added to call {dbCall.TASK_TEMPLATE.Title }.";
                        notifyModel.createdAt = DateTime.Now;
                        notifyModel.module = "calls";
                        notifyModel.moduleObjectId = dbCall.ID;
                        NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var newTag = _tagRepository.Create(tagName);
                    var tagItem = new TAG_ITEM();
                    tagItem.TAG_ID = newTag.ID;
                    tagItem.CALL_ID = dbCall.ID;
                    db.TAG_ITEM.Add(tagItem);
                    db.SaveChanges();

                    var owner = db.USERs.Find(dbCall.CallOwner);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag added";
                    notifyModel.content = $"Tag {tagName} has been added to call {dbCall.TASK_TEMPLATE.Title }.";
                    notifyModel.createdAt = DateTime.Now;
                    notifyModel.module = "calls";
                    notifyModel.moduleObjectId = dbCall.ID;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool RemoveTagFromCall(int id, int tagId)
        {
            var dbCall = db.CALLs.Find(id);
            if (dbCall != null)
            {
                var tagItem = dbCall.TAG_ITEM.Where(c => c.TAG.ID == tagId).FirstOrDefault();
                if (tagItem != null)
                {
                    var tagName = tagItem.TAG.Name;
                    db.TAG_ITEM.Remove(tagItem);
                    db.SaveChanges();

                    var owner = db.USERs.Find(dbCall.CallOwner);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag removed";
                    notifyModel.content = $"Tag {tagName} has been removed from call {dbCall.TASK_TEMPLATE.Title }.";
                    notifyModel.createdAt = DateTime.Now;
                    notifyModel.module = "calls";
                    notifyModel.moduleObjectId = dbCall.ID;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCall(int callId, CallCreateApiModel apiModel, int modifiedUser)
        {
            var dbCall = db.CALLs.Find(callId);
            if (dbCall != null)
            {
                dbCall.TASK_TEMPLATE.Description = apiModel.description;
                dbCall.TASK_TEMPLATE.IsRepeat = apiModel.isReminder;
                dbCall.TASK_TEMPLATE.RRule = apiModel.rrule;
                dbCall.TASK_TEMPLATE.Title = apiModel.title;
                dbCall.TASK_TEMPLATE.ModifiedAt = DateTime.Now;
                dbCall.TASK_TEMPLATE.ModifiedBy = modifiedUser;
                dbCall.StartTime = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.startTime);
                dbCall.TASK_TEMPLATE.StartDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.startTime);
                if(DbDateHelper.ToNullIfTooEarlyForDb(apiModel.startTime) != null)
                {
                    dbCall.TASK_TEMPLATE.DueDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.startTime).Value.AddSeconds(apiModel.duration);
                }
                //dbCall.TASK_TEMPLATE.DueDate = dbCall.TASK_TEMPLATE.CreatedAt.Value
                if (apiModel.status != 0)
                {
                    dbCall.TASK_TEMPLATE.TASK_STATUS_ID = apiModel.status;

                }
                if(apiModel.priority != 0)
                {
                    dbCall.TASK_TEMPLATE.PRIORITY_ID = apiModel.priority;

                }
                if (apiModel.owner != 0) { dbCall.CallOwner = apiModel.owner; }

                if(apiModel.purpose != 0)
                {
                    dbCall.CALLREASON_ID = apiModel.purpose;

                }

                if(apiModel.result != 0)
                {
                    dbCall.CALLRESULT_ID = apiModel.result;

                }
                dbCall.Length = apiModel.duration;
                if(apiModel.type != 0)
                {
                    dbCall.CALLTYPE_ID = apiModel.type;

                }
                dbCall.CONTACT = null;
                dbCall.LEAD = null;
                dbCall.ACCOUNT = null;
                dbCall.DEAL = null;
                dbCall.CAMPAIGN = null;

                if (apiModel.relatedAccount != 0)
                {
                    dbCall.RELATED_ACCOUNT = apiModel.relatedAccount;
                }
                if (apiModel.relatedCampaign != 0)
                {
                    dbCall.RELATED_CAMPAIGN = apiModel.relatedCampaign;
                }
                if (apiModel.relatedDeal != 0)
                {
                    dbCall.RELATED_DEAL = apiModel.relatedDeal;
                }
                if (apiModel.contact != 0)
                {
                    dbCall.CONTACT_ID = apiModel.contact;
                }
                if (apiModel.lead != 0)
                {
                    dbCall.LEAD_ID = apiModel.lead;
                }
                db.SaveChanges();

                var owner = db.USERs.Find(dbCall.CallOwner);
                var creator = db.USERs.Find(dbCall.TASK_TEMPLATE.CreatedBy);
                var modifyUser = db.USERs.Find(modifiedUser);

                try
                {
                    if (owner != null)
                    {
                        var extendedProperties = new Dictionary<string, string>();

                        if (apiModel.relatedAccount != 0)
                        {
                            var account = db.ACCOUNTs.Find(apiModel.relatedAccount);
                            if (account != null)
                            {
                                extendedProperties.Add("Account's Email", account.Email);
                                extendedProperties.Add("Account's Name", account.Name);
                                extendedProperties.Add("Account's Phone", account.Phone);
                            }

                        }
                        if (apiModel.relatedCampaign != 0)
                        {
                            var campaign = db.CAMPAIGNs.Find(apiModel.relatedCampaign);
                            if (campaign != null)
                            {
                                extendedProperties.Add("Campaign's Name", campaign.Name);
                            }
                        }
                        if (apiModel.relatedDeal != 0)
                        {
                            var deal = db.DEALs.Find(apiModel.relatedDeal);
                            if (deal != null)
                            {
                                extendedProperties.Add("Deal's Name", deal.Name);
                                extendedProperties.Add("Deal's Amount", deal.Amount.ToString());
                            }
                        }
                        if (apiModel.contact != 0)
                        {
                            var contact = db.CONTACTs.Find(apiModel.contact);
                            if (contact != null)
                            {
                                extendedProperties.Add("Contact's Name", contact.Name);
                                extendedProperties.Add("Contact's Phone", contact.Phone);
                                extendedProperties.Add("Contact's Email", contact.Email);
                                extendedProperties.Add("Contact's Skype", contact.Skype);
                                extendedProperties.Add("Assistant's Name", contact.AssistantName);
                                extendedProperties.Add("Assistant's Phone", contact.AssistantPhone);
                            }

                        }
                        if (apiModel.lead != 0)
                        {
                            var lead = db.LEADs.Find(apiModel.lead);
                            if (lead != null)
                            {
                                extendedProperties.Add("Lead's Name", lead.Name);
                                extendedProperties.Add("Lead's Phone", lead.Phone);
                                extendedProperties.Add("Lead's Email", lead.Email);
                            }

                        }

                        var calendarId = owner.CalendarId;

                        var calEvent = _googleCalendar.UpdateEvent(endDate: dbCall.TASK_TEMPLATE.DueDate, startDate: dbCall.TASK_TEMPLATE.StartDate, calendarId: calendarId, description: apiModel.description, summary: "[Call] " + apiModel.title, rrule: apiModel.rrule, recur: apiModel.isReminder, extendedProperties: extendedProperties, eventId: dbCall.TASK_TEMPLATE.EventId);

                    }
                }
                catch
                {
                }


                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Call updated";
                notifyModel.content = $"Call {dbCall.TASK_TEMPLATE.Title} has been updated by {modifyUser.Username}.";
                notifyModel.createdAt = DateTime.Now;
                notifyModel.module = "calls";
                notifyModel.moduleObjectId = dbCall.ID;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
                return true;
            }
            else
            {
                return false;
            }
        }
    
        public bool DeleteCall(int id)
        {
            var dbCall = db.CALLs.Find(id);
            if(dbCall != null)
            {
                var template = dbCall.TASK_TEMPLATE;
                var callTitle = template.Title;
                var owner = db.USERs.Find(dbCall.CallOwner);
                var creator = db.USERs.Find(dbCall.TASK_TEMPLATE.CreatedBy);
                var eventId = template.EventId;
                db.CALLs.Remove(dbCall);
                db.SaveChanges();
                db.TASK_TEMPLATE.Remove(template);
                db.SaveChanges();

                try
                {
                    _googleCalendar.DeleteEvent(calendarId: owner.CalendarId, eventId: eventId);
                }
                catch
                {

                }

                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Call removed";
                notifyModel.content = $"Call {callTitle} has been removed.";
                notifyModel.createdAt = DateTime.Now;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
                return true;
            }
            else
            {
                return false;
            }
        }

        public CALL GetCall(int id)
        {
            return db.CALLs.Find(id);
        }

        //meeting
        public int GetMeetingTemplateId(int id)
        {
            var dbMeeting = db.MEETINGs.Find(id);

            if (dbMeeting != null)
            {
                return dbMeeting.TASK_TEMPLATE.ID;
            }
            else { return 0; }
        }

        public int GetMeetingOwner(int id)
        {
            var dbMeeting = db.MEETINGs.Find(id);
            if (dbMeeting != null)
            {
                return dbMeeting.HostUser.ID;
            }
            else
            {
                return 0;
            }
        }

        public bool AddTagToMeeting(int id, string tagName)
        {
            var dbMeeting = db.MEETINGs.Find(id);
            var dbTag = _tagRepository.GetOneByName(tagName);
            if (dbMeeting != null)
            {
                if (dbTag != null)
                {
                    var tagItem = dbMeeting.TAG_ITEM.Where(c => c.TAG_ID == dbTag.ID).FirstOrDefault();
                    if (tagItem == null)
                    {
                        var newTagItem = new TAG_ITEM();
                        newTagItem.TAG_ID = dbTag.ID;
                        newTagItem.MEETING_ID = dbMeeting.ID;
                        db.TAG_ITEM.Add(newTagItem);
                        db.SaveChanges();

                        var owner = db.USERs.Find(dbMeeting.Host);

                        var notifyModel = new NotificationApiModel();
                        notifyModel.title = "Tag added";
                        notifyModel.content = $"Tag {tagName} has been added to meeting {dbMeeting.TASK_TEMPLATE.Title }.";
                        notifyModel.createdAt = DateTime.Now;
                        notifyModel.module = "meetings";
                        notifyModel.moduleObjectId = dbMeeting.ID;
                        NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var newTag = _tagRepository.Create(tagName);
                    var tagItem = new TAG_ITEM();
                    tagItem.TAG_ID = newTag.ID;
                    tagItem.MEETING_ID = dbMeeting.ID;
                    db.TAG_ITEM.Add(tagItem);
                    db.SaveChanges();
                    var owner = db.USERs.Find(dbMeeting.Host);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag added";
                    notifyModel.content = $"Tag {tagName} has been added to meeting {dbMeeting.TASK_TEMPLATE.Title }.";
                    notifyModel.createdAt = DateTime.Now;
                    notifyModel.module = "meetings";
                    notifyModel.moduleObjectId = dbMeeting.ID;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool RemoveTagFromMeeting(int id, int tagId)
        {
            var dbMeeting = db.MEETINGs.Find(id);
            if (dbMeeting != null)
            {
                var tagItem = dbMeeting.TAG_ITEM.Where(c => c.TAG.ID == tagId).FirstOrDefault();
                if (tagItem != null)
                {
                    var tagName = tagItem.TAG.Name;

                    db.TAG_ITEM.Remove(tagItem);
                    db.SaveChanges();

                    var owner = db.USERs.Find(dbMeeting.Host);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag removed";
                    notifyModel.content = $"Tag {tagName} has been removed from meeting {dbMeeting.TASK_TEMPLATE.Title }.";
                    notifyModel.createdAt = DateTime.Now;
                    notifyModel.module = "meetings";
                    notifyModel.moduleObjectId = dbMeeting.ID;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool CreateMeeting(MeetingCreateApiModel apiModel, int createdUser)
        {
            var newTemplate = new TASK_TEMPLATE();
            newTemplate.CreatedAt = DateTime.Now;
            newTemplate.CreatedBy = createdUser;
            newTemplate.ModifiedAt = DateTime.Now;
            newTemplate.Description = apiModel.description;
            newTemplate.IsRepeat = apiModel.isRepeat;
            newTemplate.RRule = apiModel.rrule;
            newTemplate.Title = apiModel.title;
            newTemplate.DueDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.to);
            newTemplate.StartDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.from);
            if(apiModel.priority != 0)
            {
                newTemplate.PRIORITY_ID = apiModel.priority;
            }
            db.TASK_TEMPLATE.Add(newTemplate);
            var newMeeting = new MEETING();
            newMeeting.FromDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.from);

            if(apiModel.host != 0)
            {
                newMeeting.Host = apiModel.host;
            }
            else
            {
                newMeeting.Host = createdUser;
            }
            newMeeting.IsAllDay = apiModel.isAllDay;
            newMeeting.IsRemindParticipant = true;
            newMeeting.Location = apiModel.location;
            newMeeting.ToDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.to);
            newMeeting.TASK_TEMPLATE = newTemplate;
            db.MEETINGs.Add(newMeeting);
            db.SaveChanges();

            var owner = db.USERs.Find(newMeeting.Host);
            var creator = db.USERs.Find(createdUser);

            try
            {
                if (owner != null)
                {
                    //var calendarId = _googleCalendar.GetId(owner.Email);
                    var calendarId = owner.CalendarId;
                    var calEvent = _googleCalendar.AddEvent(endDate: newTemplate.DueDate, startDate: newTemplate.StartDate, calendarId: calendarId, description: newTemplate.Description, summary: "[Meeting] " + newTemplate.Title, rrule: newTemplate.RRule, recur: newTemplate.IsRepeat.Value, isAllDay: newMeeting.IsAllDay.Value, location: newMeeting.Location);
                    if (calEvent != null)
                    {
                        newTemplate.EventId = calEvent.Id;
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
            }

            var notifyModel = new NotificationApiModel();
            notifyModel.title = "Meeting created";
            notifyModel.content = $"Meeting {newTemplate.Title} has been created and assigned you as host by {creator?.Username}.";
            notifyModel.createdAt = DateTime.Now;
            notifyModel.module = "meetings";
            notifyModel.moduleObjectId = newMeeting.ID;
            NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
            return true;
        }
    
        public bool UpdateMeeting(int meetingId, MeetingCreateApiModel apiModel, int modifiedUser)
        {
            var dbMeeting = db.MEETINGs.Find(meetingId);
            if(dbMeeting != null)
            {
                dbMeeting.TASK_TEMPLATE.Description = apiModel.description;
                dbMeeting.TASK_TEMPLATE.IsRepeat = apiModel.isRepeat;
                dbMeeting.TASK_TEMPLATE.RRule = apiModel.rrule;
                dbMeeting.TASK_TEMPLATE.Title = apiModel.title;
                dbMeeting.TASK_TEMPLATE.ModifiedAt = DateTime.Now;
                dbMeeting.TASK_TEMPLATE.ModifiedBy = modifiedUser;
                dbMeeting.TASK_TEMPLATE.DueDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.to);
                dbMeeting.TASK_TEMPLATE.StartDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.from);
                if(apiModel.priority != 0)
                {
                    dbMeeting.TASK_TEMPLATE.PRIORITY_ID = apiModel.priority;
                }
                if (apiModel.host != 0) { dbMeeting.Host = apiModel.host; };
                dbMeeting.IsAllDay = apiModel.isAllDay;
                dbMeeting.IsRemindParticipant = true;
                dbMeeting.Location = apiModel.location;
                dbMeeting.FromDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.from);
                dbMeeting.ToDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.to);
                db.SaveChanges();

                var owner = db.USERs.Find(dbMeeting.Host);
                var creator = db.USERs.Find(dbMeeting.TASK_TEMPLATE.CreatedBy);
                var modifyUser = db.USERs.Find(modifiedUser);

                try
                {
                    if (owner != null)
                    {
                        var calendarId = owner.CalendarId;
                        var calEvent = _googleCalendar.AddEvent(endDate: dbMeeting.TASK_TEMPLATE.DueDate, startDate: dbMeeting.TASK_TEMPLATE.StartDate, calendarId: calendarId, description: dbMeeting.TASK_TEMPLATE.Description, summary: "[Meeting] " + dbMeeting.TASK_TEMPLATE.Title, rrule: dbMeeting.TASK_TEMPLATE.RRule, recur: dbMeeting.TASK_TEMPLATE.IsRepeat.Value, isAllDay: dbMeeting.IsAllDay.Value, location: dbMeeting.Location);
                    }
                }
                catch
                {
                }

                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Meeting updated";
                notifyModel.content = $"Meeting {dbMeeting.TASK_TEMPLATE.Title} has been updated by {modifyUser.Username}.";
                notifyModel.createdAt = DateTime.Now;
                notifyModel.module = "meetings";
                notifyModel.moduleObjectId = dbMeeting.ID;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteMeeting(int id)
        {
            var dbMeeting = db.MEETINGs.Find(id);
            if (dbMeeting != null)
            {
                var template = dbMeeting.TASK_TEMPLATE;
                var meetingTitle = template.Title;
                var owner = db.USERs.Find(dbMeeting.Host);
                var creator = db.USERs.Find(dbMeeting.TASK_TEMPLATE.CreatedBy);
                var eventId = dbMeeting.TASK_TEMPLATE.EventId;
                db.MEETINGs.Remove(dbMeeting);
                db.SaveChanges();
                db.TASK_TEMPLATE.Remove(template);
                db.SaveChanges();

                try
                {
                    _googleCalendar.DeleteEvent(eventId, owner.CalendarId);
                }
                catch
                {

                }
                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Meeting removed";
                notifyModel.content = $"Meeting {meetingTitle} has been removed.";
                notifyModel.createdAt = DateTime.Now;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });

                return true;
            }
            else
            {
                return false;
            }
        }

        public MEETING GetMeeting(int id)
        {
            return db.MEETINGs.Find(id);
        }

        public bool AddParticipantToMeeting(int id, MeetingParticipantCreateModel apiModel)
        {
            var dbMeeting = db.MEETINGs.Find(id);
            if (dbMeeting != null)
            {
                var owner = db.USERs.Find(dbMeeting.Host);
                if (apiModel.contactId != 0)
                {
                    var participant = new MEETING_PARTICIPANT();
                    participant.CONTACT_ID = apiModel.contactId;
                    participant.MEETING_ID = dbMeeting.ID;
                    db.MEETING_PARTICIPANT.Add(participant);
                    db.SaveChanges();
                    
                    var contactAdded = db.CONTACTs.Find(participant.CONTACT_ID);

                    //send email

                    try
                    {
                        var calendarId = dbMeeting.HostUser.CalendarId;
                        var eventId = dbMeeting.TASK_TEMPLATE.EventId;
                        var htmlLink = _googleCalendar.GetHtmlLinkAsync(calendarId, eventId);
                        htmlLink.ContinueWith((result) =>
                        {
                            var publishLink = _googleCalendar.Publish(calendarId, result.Result.HtmlLink);
                            _emailManager.Recipients.Clear();
                            _emailManager.Recipients.Add(contactAdded.Email);
                            _emailManager.Title = StaticStrings.INVITED_MEETING;
                            _emailManager.Content =
                                $"<p>Our representative {owner.FirstName + " " + owner.LastName} has invited you to a meeting.</p>" +
                                $"<h3>Meeting details:</h3>" +
                                $"<ul>" +
                                    $"<li>Meeting: {dbMeeting.TASK_TEMPLATE.Title}</li>" +
                                    $"<li>From: {dbMeeting.TASK_TEMPLATE.StartDate.Value.ToUniversalTime()}</li>" +
                                    $"<li>To: {dbMeeting.TASK_TEMPLATE.DueDate.Value.ToUniversalTime()}</li>" +
                                    $"<li>Location: {dbMeeting.Location}</li>" +
                                    $"<li>Host: {dbMeeting.HostUser.FirstName + " " + dbMeeting.HostUser.LastName}</li></ul>" +

                                $"<div style='margin-top: 40px;'><a style='-webkit-appearance:button; -moz-appearance:button; appearance:button; text-decoration:none; background-color: white; color: #D93915; border: 1px solid #D93915; border-radius: 5px; padding: 1em 1.5em; text-transform: uppercase;' href='{publishLink}'>Add event to calendar</a></div>";
                            _emailManager.SendEmail();
                        });
                    }
                    catch
                    {

                    }

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Participant added";
                    notifyModel.content = $"Contact {contactAdded.Name} has been added to meeting {dbMeeting.TASK_TEMPLATE.Title}.";
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                }
                if(apiModel.leadId != 0)
                {
                    var participant = new MEETING_PARTICIPANT();
                    participant.LEAD_ID = apiModel.leadId;
                    participant.MEETING_ID = dbMeeting.ID;
                    db.MEETING_PARTICIPANT.Add(participant);
                    db.SaveChanges();

                    var leadAdded = db.LEADs.Find(participant.LEAD_ID);

                    try
                    {
                        var calendarId = dbMeeting.HostUser.CalendarId;
                        var eventId = dbMeeting.TASK_TEMPLATE.EventId;
                        var htmlLink = _googleCalendar.GetHtmlLinkAsync(calendarId, eventId);
                        htmlLink.ContinueWith((result) =>
                        {
                            var publishLink = _googleCalendar.Publish(calendarId, result.Result.HtmlLink);
                            _emailManager.Recipients.Clear();
                            _emailManager.Recipients.Add(leadAdded.Email);
                            _emailManager.Title = StaticStrings.INVITED_MEETING;
                            _emailManager.Content =
                                $"<p>Our representative {owner.FirstName + " " + owner.LastName} has invited you to a meeting.</p>" +
                                $"<h3>Meeting details:</h3>" +
                                $"<ul>" +
                                    $"<li>Meeting: {dbMeeting.TASK_TEMPLATE.Title}</li>" +
                                    $"<li>From: {dbMeeting.TASK_TEMPLATE.StartDate.Value.ToUniversalTime()}</li>" +
                                    $"<li>To: {dbMeeting.TASK_TEMPLATE.DueDate.Value.ToUniversalTime()}</li>" +
                                    $"<li>Location: {dbMeeting.Location}</li>" +
                                    $"<li>Host: {dbMeeting.HostUser.FirstName + " " + dbMeeting.HostUser.LastName}</li></ul>" +

                                $"<div style='margin-top: 40px;'><a style='-webkit-appearance:button; -moz-appearance:button; appearance:button; text-decoration:none; background-color: white; color: #D93915; border: 1px solid #D93915; border-radius: 5px; padding: 1em 1.5em; text-transform: uppercase;' href='{publishLink}'>Add event to calendar</a></div>";
                            _emailManager.SendEmail();
                        });
                    }
                    catch
                    {

                    }


                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Participant added";
                    notifyModel.content = $"Lead {leadAdded.Name} has been added to meeting {dbMeeting.TASK_TEMPLATE.Title}.";
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner });

                }
                if(apiModel.userId != 0)
                {
                    var participant = new MEETING_PARTICIPANT();
                    participant.USER_ID = apiModel.userId;
                    participant.MEETING_ID = dbMeeting.ID;
                    db.MEETING_PARTICIPANT.Add(participant);
                    db.SaveChanges();

                    var userAdded = db.USERs.Find(participant.USER_ID);

                    var calId = userAdded.CalendarId;
                    var calEvent = _googleCalendar.AddEvent(endDate: dbMeeting.TASK_TEMPLATE.DueDate, startDate: dbMeeting.TASK_TEMPLATE.StartDate, description: dbMeeting.TASK_TEMPLATE.Description, summary: "[Meeting] " + dbMeeting.TASK_TEMPLATE.Title, rrule: dbMeeting.TASK_TEMPLATE.RRule, recur: dbMeeting.TASK_TEMPLATE.IsRepeat.Value, isAllDay: dbMeeting.IsAllDay.Value, location: dbMeeting.Location, calendarId: calId);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Participant added";
                    notifyModel.content = $"User {userAdded.Username} has been added to meeting {dbMeeting.TASK_TEMPLATE.Title}.";
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner });

                    var notifyModel1 = new NotificationApiModel();
                    notifyModel1.title = "Added to meeting";
                    notifyModel1.content = $"You have been added to meeting {dbMeeting.TASK_TEMPLATE.Title}";
                    notifyModel1.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel1, new List<USER> { userAdded });

                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveParticipantFromMeeting(int id, MeetingParticipantCreateModel apiModel)
        {
            var dbMeeting = db.MEETINGs.Find(id);
            if (dbMeeting != null)
            {
                var owner = db.USERs.Find(dbMeeting.Host);

                if (apiModel.contactId != 0)
                {
                    var participant = db.MEETING_PARTICIPANT.Where(c => c.CONTACT.ID == apiModel.contactId && c.MEETING.ID == dbMeeting.ID).FirstOrDefault();
                    if (participant != null)
                    {
                        var contactName = participant.CONTACT.Name;
                        db.MEETING_PARTICIPANT.Remove(participant);
                        db.SaveChanges();

                        var notifyModel = new NotificationApiModel();
                        notifyModel.title = "Participant removed";
                        notifyModel.content = $"Contact {contactName} has been removed from meeting {dbMeeting.TASK_TEMPLATE.Title}.";
                        notifyModel.createdAt = DateTime.Now;
                        NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                    }
                    else
                    {
                        return false;
                    }
                }
                if (apiModel.leadId != 0)
                {
                    var participant = db.MEETING_PARTICIPANT.Where(c => c.LEAD.ID == apiModel.leadId && c.MEETING.ID == dbMeeting.ID).FirstOrDefault();
                    if (participant != null)
                    {
                        var leadName = participant.LEAD.Name;
                        db.MEETING_PARTICIPANT.Remove(participant);
                        db.SaveChanges();

                        var notifyModel = new NotificationApiModel();
                        notifyModel.title = "Participant removed";
                        notifyModel.content = $"Lead {leadName} has been removed from meeting {dbMeeting.TASK_TEMPLATE.Title}.";
                        notifyModel.createdAt = DateTime.Now;
                        NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                    }
                    else
                    {
                        return false;
                    }
                }
                if (apiModel.userId != 0)
                {
                    var participant = db.MEETING_PARTICIPANT.Where(c => c.USER.ID == apiModel.userId && c.MEETING.ID == dbMeeting.ID).FirstOrDefault();
                    if (participant != null)
                    {
                        var username = participant.USER.Username;
                        db.MEETING_PARTICIPANT.Remove(participant);
                        db.SaveChanges();

                        var notifyModel = new NotificationApiModel();
                        notifyModel.title = "Participant removed";
                        notifyModel.content = $"User {username} has been removed from meeting {dbMeeting.TASK_TEMPLATE.Title}.";
                        notifyModel.createdAt = DateTime.Now;
                        NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //task
        public int GetTaskTemplateId(int id)
        {
            var dbTask = db.TASKs.Find(id);

            if (dbTask != null)
            {
                return dbTask.TASK_TEMPLATE.ID;
            }
            else { return 0; }
        }

        public int GetTaskOwner(int id)
        {
            var dbTask = db.TASKs.Find(id);
            if (dbTask != null)
            {
                return dbTask.USER.ID;
            }
            else
            {
                return 0;
            }
        }

        public bool AddTagToTask(int id, string tagName)
        {
            var dbTask = db.TASKs.Find(id);
            var dbTag = _tagRepository.GetOneByName(tagName);
            if (dbTask != null)
            {
                if (dbTag != null)
                {
                    var tagItem = dbTask.TAG_ITEM.Where(c => c.TAG_ID == dbTag.ID).FirstOrDefault();
                    if (tagItem == null)
                    {
                        var newTagItem = new TAG_ITEM();
                        newTagItem.TAG_ID = dbTag.ID;
                        newTagItem.TASK_ID = dbTask.ID;
                        db.TAG_ITEM.Add(newTagItem);
                        db.SaveChanges();

                        var owner = db.USERs.Find(dbTask.TaskOwner);

                        var notifyModel = new NotificationApiModel();
                        notifyModel.title = "Tag added";
                        notifyModel.content = $"Tag {tagName} has been added to task {dbTask.TASK_TEMPLATE.Title }.";
                        notifyModel.createdAt = DateTime.Now;
                        notifyModel.module = "tasks";
                        notifyModel.moduleObjectId = dbTask.ID;
                        NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var newTag = _tagRepository.Create(tagName);
                    var tagItem = new TAG_ITEM();
                    tagItem.TAG_ID = newTag.ID;
                    tagItem.TASK_ID = dbTask.ID;
                    db.TAG_ITEM.Add(tagItem);
                    db.SaveChanges();

                    var owner = db.USERs.Find(dbTask.TaskOwner);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag added";
                    notifyModel.content = $"Tag {tagName} has been added to task {dbTask.TASK_TEMPLATE.Title }.";
                    notifyModel.createdAt = DateTime.Now;
                    notifyModel.module = "tasks";
                    notifyModel.moduleObjectId = dbTask.ID;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public bool RemoveTagFromTask(int id, int tagId)
        {
            var dbTask = db.TASKs.Find(id);
            if (dbTask != null)
            {
                var tagItem = dbTask.TAG_ITEM.Where(c => c.TAG.ID == tagId).FirstOrDefault();
                if (tagItem != null)
                {
                    var tagName = tagItem.TAG.Name;

                    db.TAG_ITEM.Remove(tagItem);
                    db.SaveChanges();

                    var owner = db.USERs.Find(dbTask.TaskOwner);

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag removed";
                    notifyModel.content = $"Tag {tagName} has been removed from meeting {dbTask.TASK_TEMPLATE.Title }.";
                    notifyModel.createdAt = DateTime.Now;
                    notifyModel.module = "tasks";
                    notifyModel.moduleObjectId = dbTask.ID;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool CreateTask(TaskCreateApiModel apiModel, int createdUser)
        {
            //create new template
            var newTemplate = new TASK_TEMPLATE();
            newTemplate.CreatedAt = DateTime.Now;
            newTemplate.CreatedBy = createdUser;
            newTemplate.ModifiedAt = DateTime.Now;
            newTemplate.Description = apiModel.description;
            newTemplate.IsRepeat = apiModel.isReminder;
            newTemplate.RRule = apiModel.rrule;
            newTemplate.Title = apiModel.title;
            newTemplate.StartDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.startDate);
            newTemplate.DueDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.dueDate);
            if(apiModel.status != 0)
            {
                newTemplate.TASK_STATUS_ID = apiModel.status;

            }
            if (apiModel.priority != 0)
            {
                newTemplate.PRIORITY_ID = apiModel.priority;
            }
            db.TASK_TEMPLATE.Add(newTemplate);
            //create call
            var newTask = new TASK();
            newTask.EndOn = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.dueDate);
            newTask.TASK_TEMPLATE = newTemplate;
            newTask.TaskOwner = apiModel.owner != 0 ? apiModel.owner : createdUser;
            if (apiModel.relatedAccount != 0)
            {
                newTask.RELATED_ACCOUNT = apiModel.relatedAccount;
            }
            if (apiModel.relatedCampaign != 0)
            {
                newTask.RELATED_CAMPAIGN = apiModel.relatedCampaign;
            }
            if (apiModel.relatedDeal != 0)
            {
                newTask.RELATED_DEAL = apiModel.relatedDeal;
            }
            if (apiModel.contact != 0)
            {
                newTask.CONTACT_ID = apiModel.contact;
            }
            if (apiModel.lead != 0)
            {
                newTask.LEAD_ID = apiModel.lead;
            }
            db.TASKs.Add(newTask);
            db.SaveChanges();


            var owner = db.USERs.Find(newTask.TaskOwner);
            var creator = db.USERs.Find(createdUser);
            try
            {
                if(owner != null)
                {
                    var extendedProperties = new Dictionary<string, string>();

                    if (apiModel.relatedAccount != 0)
                    {
                        var account = db.ACCOUNTs.Find(apiModel.relatedAccount);
                        if(account != null)
                        {
                            extendedProperties.Add("Account's Email", account.Email);
                            extendedProperties.Add("Account's Name", account.Name);
                            extendedProperties.Add("Account's Phone", account.Phone);
                        }

                    }
                    if (apiModel.relatedCampaign != 0)
                    {
                        var campaign = db.CAMPAIGNs.Find(apiModel.relatedCampaign);
                        if(campaign != null)
                        {
                            extendedProperties.Add("Campaign's Name", campaign.Name);
                        }
                    }
                    if (apiModel.relatedDeal != 0)
                    {
                        var deal = db.DEALs.Find(apiModel.relatedDeal);
                        if(deal != null)
                        {
                            extendedProperties.Add("Deal's Name", deal.Name);
                            extendedProperties.Add("Deal's Amount", deal.Amount.ToString());
                        }
                    }
                    if (apiModel.contact != 0)
                    {
                        var contact = db.CONTACTs.Find(apiModel.contact);
                        if(contact != null)
                        {
                            extendedProperties.Add("Contact's Name", contact.Name);
                            extendedProperties.Add("Contact's Phone", contact.Phone);
                            extendedProperties.Add("Contact's Email", contact.Email);
                            extendedProperties.Add("Contact's Skype", contact.Skype);
                            extendedProperties.Add("Assistant's Name", contact.AssistantName);
                            extendedProperties.Add("Assistant's Phone", contact.AssistantPhone);
                        }

                    }
                    if (apiModel.lead != 0)
                    {
                        var lead = db.LEADs.Find(apiModel.lead);
                        if(lead != null)
                        {
                            extendedProperties.Add("Lead's Name", lead.Name);
                            extendedProperties.Add("Lead's Phone", lead.Phone);
                            extendedProperties.Add("Lead's Email", lead.Email);
                        }
                       
                    }

                    var calendarId = owner.CalendarId;
                    var calEvent = _googleCalendar.AddEvent(endDate: newTemplate.DueDate, startDate: newTemplate.StartDate, calendarId: calendarId, description: newTemplate.Description, summary:"[Task] " + newTemplate.Title, rrule: newTemplate.RRule, recur: newTemplate.IsRepeat.Value, extendedProperties: extendedProperties);
                    if(calEvent != null)
                    {
                        newTemplate.EventId = calEvent.Id;
                        db.SaveChanges();
                    }
                }
            }
            catch
            {
            }
            var notifyModel = new NotificationApiModel();
            notifyModel.title = "Task created";
            notifyModel.content = $"Task {newTemplate.Title} has been created and assigned to you by {creator?.Username}.";
            notifyModel.createdAt = DateTime.Now;
            notifyModel.module = "tasks";
            notifyModel.moduleObjectId = newTask.ID;
            NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
            return true;
        }

        public bool UpdateTask(int taskId, TaskCreateApiModel apiModel, int modifiedUser)
        {
            var dbTask = db.TASKs.Find(taskId);
            if (dbTask != null)
            {
                dbTask.TASK_TEMPLATE.Description = apiModel.description;
                dbTask.TASK_TEMPLATE.IsRepeat = apiModel.isReminder;
                dbTask.TASK_TEMPLATE.RRule = apiModel.rrule;
                dbTask.TASK_TEMPLATE.Title = apiModel.title;
                dbTask.TASK_TEMPLATE.ModifiedAt = DateTime.Now;
                dbTask.TASK_TEMPLATE.ModifiedBy = modifiedUser;
                dbTask.TASK_TEMPLATE.StartDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.startDate);
                dbTask.TASK_TEMPLATE.DueDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.dueDate);
                if(apiModel.status != 0)
                {
                    dbTask.TASK_TEMPLATE.TASK_STATUS_ID = apiModel.status;

                }
                if (apiModel.priority != 0)
                {
                    dbTask.TASK_TEMPLATE.PRIORITY_ID = apiModel.priority;
                }

                if (apiModel.owner != 0) { dbTask.TaskOwner = apiModel.owner; }

                dbTask.EndOn = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.dueDate);
                dbTask.CONTACT = null;
                dbTask.LEAD = null;
                dbTask.ACCOUNT = null;
                dbTask.DEAL = null;
                dbTask.CAMPAIGN = null;

                if (apiModel.relatedAccount != 0)
                {
                    dbTask.RELATED_ACCOUNT = apiModel.relatedAccount;
                }
                if (apiModel.relatedCampaign != 0)
                {
                    dbTask.RELATED_CAMPAIGN = apiModel.relatedCampaign;
                }
                if (apiModel.relatedDeal != 0)
                {
                    dbTask.RELATED_DEAL = apiModel.relatedDeal;
                }
                if (apiModel.contact != 0)
                {
                    dbTask.CONTACT_ID = apiModel.contact;
                }
                if (apiModel.lead != 0)
                {
                    dbTask.LEAD_ID = apiModel.lead;
                }
                db.SaveChanges();

                var owner = db.USERs.Find(dbTask.TaskOwner);
                var creator = db.USERs.Find(dbTask.TASK_TEMPLATE.CreatedBy);
                var modifyUser = db.USERs.Find(modifiedUser);

                try
                {
                    if (owner != null)
                    {
                        var extendedProperties = new Dictionary<string, string>();

                        if (apiModel.relatedAccount != 0)
                        {
                            var account = db.ACCOUNTs.Find(apiModel.relatedAccount);
                            if (account != null)
                            {
                                extendedProperties.Add("Account's Email", account.Email);
                                extendedProperties.Add("Account's Name", account.Name);
                                extendedProperties.Add("Account's Phone", account.Phone);
                            }

                        }
                        if (apiModel.relatedCampaign != 0)
                        {
                            var campaign = db.CAMPAIGNs.Find(apiModel.relatedCampaign);
                            if (campaign != null)
                            {
                                extendedProperties.Add("Campaign's Name", campaign.Name);
                            }
                        }
                        if (apiModel.relatedDeal != 0)
                        {
                            var deal = db.DEALs.Find(apiModel.relatedDeal);
                            if (deal != null)
                            {
                                extendedProperties.Add("Deal's Name", deal.Name);
                                extendedProperties.Add("Deal's Amount", deal.Amount.ToString());
                            }
                        }
                        if (apiModel.contact != 0)
                        {
                            var contact = db.CONTACTs.Find(apiModel.contact);
                            if (contact != null)
                            {
                                extendedProperties.Add("Contact's Name", contact.Name);
                                extendedProperties.Add("Contact's Phone", contact.Phone);
                                extendedProperties.Add("Contact's Email", contact.Email);
                                extendedProperties.Add("Contact's Skype", contact.Skype);
                                extendedProperties.Add("Assistant's Name", contact.AssistantName);
                                extendedProperties.Add("Assistant's Phone", contact.AssistantPhone);
                            }

                        }
                        if (apiModel.lead != 0)
                        {
                            var lead = db.LEADs.Find(apiModel.lead);
                            if (lead != null)
                            {
                                extendedProperties.Add("Lead's Name", lead.Name);
                                extendedProperties.Add("Lead's Phone", lead.Phone);
                                extendedProperties.Add("Lead's Email", lead.Email);
                            }

                        }

                        var calendarId = owner.CalendarId;
                        var calEvent = _googleCalendar.UpdateEvent(endDate: dbTask.TASK_TEMPLATE.DueDate, startDate: dbTask.TASK_TEMPLATE.StartDate, calendarId: calendarId, description: apiModel.description, summary: "[Task] " + apiModel.title, rrule: apiModel.rrule, recur: apiModel.isReminder, extendedProperties: extendedProperties, eventId: dbTask.TASK_TEMPLATE.EventId);
                    }
                }
                catch
                {
                }


                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Task updated";
                notifyModel.content = $"Task {dbTask.TASK_TEMPLATE.Title} has been updated by {modifyUser.Username}.";
                notifyModel.createdAt = DateTime.Now;
                notifyModel.module = "tasks";
                notifyModel.moduleObjectId = dbTask.ID;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTask(int id)
        {
            var dbTask = db.TASKs.Find(id);
            if (dbTask != null)
            {
                var template = dbTask.TASK_TEMPLATE;
                var taskTitle = template.Title;
                var owner = db.USERs.Find(dbTask.TaskOwner);
                var creator = db.USERs.Find(dbTask.TASK_TEMPLATE.CreatedBy);
                var eventId = dbTask.TASK_TEMPLATE.EventId;
                db.TASKs.Remove(dbTask);
                db.SaveChanges();
                db.TASK_TEMPLATE.Remove(template);
                db.SaveChanges();

                try
                {
                    _googleCalendar.DeleteEvent(calendarId: owner.CalendarId, eventId: eventId );
                }
                catch
                {
                    throw;
                }
                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Task removed";
                notifyModel.content = $"Task {taskTitle} has been removed.";
                notifyModel.createdAt = DateTime.Now;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, creator });
                return true;
            }
            else
            {
                return false;
            }
        }

        public TASK GetTask(int id)
        {
            return db.TASKs.Find(id);
        }
    }
}