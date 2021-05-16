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

namespace Backend.Repository
{
    public class TaskTemplateRepository
    {
        DatabaseContext db = new DatabaseContext();
        TagRepository _tagRepository = new TagRepository();
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
                newTemplate.DueDate = startTime.Value.AddSeconds(apiModel.duration);
            }
            //newTemplate.DueDate = DbDateHelper.ToNullIfTooEarlyForDb(apiModel.startTime);
            //newTemplate.DueDate = DateTime.Now.AddSeconds(apiModel.duration);
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
                //dbCall.TASK_TEMPLATE.DueDate = dbCall.TASK_TEMPLATE.CreatedAt.Value
                if (dbCall.TASK_TEMPLATE.CreatedAt.HasValue)
                {
                    dbCall.TASK_TEMPLATE.DueDate = dbCall.TASK_TEMPLATE.CreatedAt.Value.AddSeconds(apiModel.duration);
                }
                if(apiModel.status != 0)
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
                db.CALLs.Remove(dbCall);
                db.SaveChanges();
                db.TASK_TEMPLATE.Remove(template);
                db.SaveChanges();

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

                db.MEETINGs.Remove(dbMeeting);
                db.SaveChanges();
                db.TASK_TEMPLATE.Remove(template);
                db.SaveChanges();

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

                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Participant added";
                    notifyModel.content = $"User {userAdded.Username} has been added to meeting {dbMeeting.TASK_TEMPLATE.Title}.";
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner });
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

                db.TASKs.Remove(dbTask);
                db.SaveChanges();
                db.TASK_TEMPLATE.Remove(template);
                db.SaveChanges();

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