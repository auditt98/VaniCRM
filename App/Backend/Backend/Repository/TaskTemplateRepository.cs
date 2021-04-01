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

namespace Backend.Repository
{
    public class TaskTemplateRepository
    {
        DatabaseContext db = new DatabaseContext();
        TagRepository _tagRepository = new TagRepository();
        //get user's tasks

        public IEnumerable<TASK_TEMPLATE> GetUserTaskTemplate(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            var dbUser = db.USERs.Find(userID);
            var templates = dbUser.TaskTemplateCreated.ToList();
            foreach (var meeting in dbUser.MEETING_PARTICIPANT)
            {
                templates.Add(meeting.MEETING.TASK_TEMPLATE);
            }

            if (pageSize == 0)
            {
                pageSize = templates.Count();
            }

            if (String.IsNullOrEmpty(q))
            {
                return templates.OrderBy(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var result = templates.Where(c => c.Title.ToLower().Contains(q.ToLower())).OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return result;
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
            newTemplate.CreatedBy = createdUser;
            newTemplate.Description = apiModel.description;
            newTemplate.IsRepeat = apiModel.isReminder;
            newTemplate.RRule = apiModel.rrule;
            newTemplate.Title = apiModel.title;
            newTemplate.TASK_STATUS_ID = apiModel.status;
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
            newCall.StartTime = apiModel.startTime;
            db.CALLs.Add(newCall);
            db.SaveChanges();
            
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
                    if (tagItem != null)
                    {
                        var newTagItem = new TAG_ITEM();
                        newTagItem.TAG_ID = dbTag.ID;
                        newTagItem.CALL_ID = dbCall.ID;
                        db.TAG_ITEM.Add(newTagItem);
                        db.SaveChanges();
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
                    db.TAG_ITEM.Remove(tagItem);
                    db.SaveChanges();
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
                dbCall.TASK_TEMPLATE.TASK_STATUS_ID = apiModel.status;

                if(apiModel.owner != 0) { dbCall.CallOwner = apiModel.owner; }
                dbCall.CALLREASON_ID = apiModel.purpose;
                dbCall.CALLRESULT_ID = apiModel.result;
                dbCall.Length = apiModel.duration;
                dbCall.CALLTYPE_ID = apiModel.type;
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
                db.CALLs.Remove(dbCall);
                db.SaveChanges();
                db.TASK_TEMPLATE.Remove(template);
                db.SaveChanges();
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
                    if (tagItem != null)
                    {
                        var newTagItem = new TAG_ITEM();
                        newTagItem.TAG_ID = dbTag.ID;
                        newTagItem.MEETING_ID = dbMeeting.ID;
                        db.TAG_ITEM.Add(newTagItem);
                        db.SaveChanges();
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
                    db.TAG_ITEM.Remove(tagItem);
                    db.SaveChanges();
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
            newTemplate.Description = apiModel.description;
            newTemplate.IsRepeat = apiModel.isRepeat;
            newTemplate.RRule = apiModel.rrule;
            newTemplate.Title = apiModel.title;
            db.TASK_TEMPLATE.Add(newTemplate);
            var newMeeting = new MEETING();
            newMeeting.FromDate = apiModel.from;
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
            newMeeting.ToDate = apiModel.to;
            newMeeting.TASK_TEMPLATE = newTemplate;
            db.MEETINGs.Add(newMeeting);
            db.SaveChanges();
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
                if (apiModel.host != 0) { dbMeeting.Host = apiModel.host; };
                dbMeeting.IsAllDay = apiModel.isAllDay;
                dbMeeting.IsRemindParticipant = true;
                dbMeeting.Location = apiModel.location;
                dbMeeting.ToDate = apiModel.to;
                db.SaveChanges();
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
                db.MEETINGs.Remove(dbMeeting);
                db.SaveChanges();
                db.TASK_TEMPLATE.Remove(template);
                db.SaveChanges();
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
                if (apiModel.contactId != 0)
                {
                    var participant = new MEETING_PARTICIPANT();
                    participant.CONTACT_ID = apiModel.contactId;
                    participant.MEETING_ID = dbMeeting.ID;
                    db.MEETING_PARTICIPANT.Add(participant);
                    db.SaveChanges();
                }
                if(apiModel.leadId != 0)
                {
                    var participant = new MEETING_PARTICIPANT();
                    participant.LEAD_ID = apiModel.leadId;
                    participant.MEETING_ID = dbMeeting.ID;
                    db.MEETING_PARTICIPANT.Add(participant);
                    db.SaveChanges();
                }
                if(apiModel.userId != 0)
                {
                    var participant = new MEETING_PARTICIPANT();
                    participant.USER_ID = apiModel.userId;
                    participant.MEETING_ID = dbMeeting.ID;
                    db.MEETING_PARTICIPANT.Add(participant);
                    db.SaveChanges();
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
                if (apiModel.contactId != 0)
                {
                    var participant = db.MEETING_PARTICIPANT.Where(c => c.CONTACT.ID == apiModel.contactId && c.MEETING.ID == dbMeeting.ID).FirstOrDefault();
                    if (participant != null)
                    {
                        db.MEETING_PARTICIPANT.Remove(participant);
                        db.SaveChanges();
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
                        db.MEETING_PARTICIPANT.Remove(participant);
                        db.SaveChanges();
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
                        db.MEETING_PARTICIPANT.Remove(participant);
                        db.SaveChanges();
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
    }
}