using Backend.Domain;
using Backend.Extensions;
using Backend.Models.ApiModel;
using Backend.Repository;
using Backend.Resources;
using Backend.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class TaskTemplateService
    {
        TaskTemplateRepository _taskTemplateRepository = new TaskTemplateRepository();
        CallValidator _callValidator = new CallValidator();
        MeetingValidator _meetingValidator = new MeetingValidator();
        TaskValidator _taskValidator = new TaskValidator();
        PriorityRepository _priorityRepository = new PriorityRepository();
        GoogleCalendar googleCalendar = new GoogleCalendar();
        public (List<TASK_TEMPLATE> tasks, Pager pageInfo) GetUserTaskTemplate(int userID, string q = "", int currentPage = 1, int pageSize = 0, string status = "")
        {
            return _taskTemplateRepository.GetUserTaskTemplate(userID, q, currentPage, pageSize, status);
        }

        public CallBlankApiModel PrepareNewCall()
        {
            var apiModel = new CallBlankApiModel();

            apiModel.statuses = _taskTemplateRepository.GetAllTaskStatuses().Select(c => new TaskStatus() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.types = _taskTemplateRepository.GetAllCallTypes().Select(c => new CallType() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.results = _taskTemplateRepository.GetAllCallResults().Select(c => new CallResult() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.purposes = _taskTemplateRepository.GetAllCallReasons().Select(c => new CallPurpose() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.priorities = _priorityRepository.GetAllPriorities().Select(c => new PrioritySelectionApiModel() { id = c.ID, name = c.Name, selected = false }).ToList();

            return apiModel;
        }

        public bool CreateCall(CallCreateApiModel apiModel, int createdUser)
        {
            var validator = _callValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _taskTemplateRepository.CreateCall(apiModel, createdUser);
            }
            return false;
        }

        public bool UpdateCall(int id, CallCreateApiModel apiModel, int modifiedUser)
        {
            var validator = _callValidator.Validate(apiModel);

            if (validator.IsValid)
            {
                return _taskTemplateRepository.UpdateCall(id, apiModel, modifiedUser);
            }
            return false;
        }

        public bool DeleteCall(int id)
        {
            return _taskTemplateRepository.DeleteCall(id);
        }

        public CallDetailApiModel GetCall(int id)
        {
            var dbCall = _taskTemplateRepository.GetCall(id);
            if(dbCall != null)
            {
                var apiModel = new CallDetailApiModel();
                apiModel.id = dbCall.ID;
                if(dbCall.CONTACT != null)
                {
                    apiModel.contact = new ContactLinkApiModel() { id = dbCall.CONTACT.ID, name = dbCall.CONTACT.Name, email = dbCall.CONTACT.Email };
                }
                if(dbCall.LEAD != null)
                {
                    apiModel.lead = new LeadLinkApiModel() { id = dbCall.LEAD.ID, name = dbCall.LEAD.Name, email = dbCall.LEAD.Email };

                }
                if(dbCall.ACCOUNT != null)
                {
                    apiModel.relatedAccount = new AccountLinkApiModel() { id = dbCall.ACCOUNT.ID, name = dbCall.ACCOUNT.Name, email = dbCall.ACCOUNT.Email };
                }
                if(dbCall.DEAL != null)
                {
                    apiModel.relatedDeal = new DealLinkApiModel() { id = dbCall.DEAL.ID, name = dbCall.DEAL.Name };
                }
                if(dbCall.CAMPAIGN != null)
                {
                    apiModel.relatedCampaign = new CampaignLinkApiModel() { id = dbCall.CAMPAIGN.ID, name = dbCall.CAMPAIGN.Name };
                }


                apiModel.purposes = _taskTemplateRepository.GetAllCallReasons().Select(c => new CallPurpose() { id = c.ID, name = c.Name, selected = dbCall.CALL_REASON != null ? c.ID == dbCall.CALL_REASON.ID : false }).ToList();
                apiModel.types = _taskTemplateRepository.GetAllCallTypes().Select(c => new CallType() { id = c.ID, name = c.Name, selected = dbCall.CALL_TYPE != null ? c.ID == dbCall.CALL_TYPE.ID : false }).ToList();
                apiModel.statuses = _taskTemplateRepository.GetAllTaskStatuses().Select(c => new TaskStatus() { id = c.ID, name = c.Name, selected = dbCall.TASK_TEMPLATE.TASK_STATUS != null ? c.ID == dbCall.TASK_TEMPLATE.TASK_STATUS.ID : false }).ToList();
                apiModel.results = _taskTemplateRepository.GetAllCallResults().Select(c => new CallResult() { id = c.ID, name = c.Name, selected = dbCall.CALL_RESULT != null ? c.ID == dbCall.CALL_RESULT.ID : false}).ToList();
                apiModel.priorities = _priorityRepository.GetAllPriorities().Select(c => new PrioritySelectionApiModel() { id = c.ID, name = c.Name, selected = dbCall.TASK_TEMPLATE.PRIORITY != null ? dbCall.TASK_TEMPLATE.PRIORITY.ID == c.ID : false }).ToList();


                apiModel.title = dbCall.TASK_TEMPLATE.Title;
                apiModel.duration = dbCall.Length.GetValueOrDefault();
                apiModel.startTime = dbCall.StartTime.GetValueOrDefault();
                apiModel.isRepeat = dbCall.TASK_TEMPLATE.IsRepeat.GetValueOrDefault();
                apiModel.rrule = dbCall.TASK_TEMPLATE.RRule;
                apiModel.description = dbCall.TASK_TEMPLATE.Description;

                if(dbCall.Owner != null)
                {
                    apiModel.owner = new UserLinkApiModel() { id = dbCall.Owner.ID, username = dbCall.Owner.Username, email = dbCall.Owner.Email };
                }
                apiModel.createdAt = dbCall.TASK_TEMPLATE.CreatedAt.GetValueOrDefault();
                apiModel.modifiedAt = dbCall.TASK_TEMPLATE.ModifiedAt.GetValueOrDefault();
                apiModel.createdBy = new UserLinkApiModel() { id = dbCall.TASK_TEMPLATE.CreatedUser.ID, username = dbCall.TASK_TEMPLATE.CreatedUser.Username, email = dbCall.TASK_TEMPLATE.CreatedUser.Email };
                if(dbCall.TASK_TEMPLATE.ModifiedUSer != null)
                {
                    apiModel.modifiedBy = new UserLinkApiModel() { id = dbCall.TASK_TEMPLATE.ModifiedUSer.ID, username = dbCall.TASK_TEMPLATE.ModifiedUSer.Username, email = dbCall.TASK_TEMPLATE.ModifiedUSer.Email };

                }
                apiModel.tags = dbCall.TAG_ITEM.Select(c => new TagApiModel() { id = c.TAG.ID, name = c.TAG.Name }).ToList();
                apiModel.notes = dbCall.TASK_TEMPLATE.NOTEs.Select(c => new NoteApiModel() { id = c.ID, avatar = $"{StaticStrings.ServerHost}avatar?fileName={c.USER.Avatar}", body = c.NoteBody, createdAt = c.CreatedAt.GetValueOrDefault(), createdBy = new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username, email = c.USER.Email }, files = c.FILEs.Select(f => new FileApiModel() { id = f.ID, fileName = f.FileName, size = f.FileSize.Value.ToString() + " KB", url = StaticStrings.ServerHost + "files/" + f.ID }).ToList() }).ToList();
                var calId = dbCall.Owner.CalendarId;
                var eventId = dbCall.TASK_TEMPLATE.EventId;
                apiModel.link = googleCalendar.GetHtmlLink(calId, eventId);
                return apiModel;
            }
            else
            {
                return null;
            }
        }

        public int GetCallTemplateId(int id)
        {
            return _taskTemplateRepository.GetCallTemplateId(id);
        }

        public int GetCallOwner(int id)
        {
            return _taskTemplateRepository.GetCallOwner(id);
        }

        public bool AddTagToCall(int id, string tagName)
        {
            return _taskTemplateRepository.AddTagToCall(id, tagName);
        }

        public bool RemoveTagFromCall(int id, int tagId)
        {
            return _taskTemplateRepository.RemoveTagFromCall(id, tagId);
        }


        //meeting

        public int GetMeetingTemplateId(int id)
        {
            return _taskTemplateRepository.GetMeetingTemplateId(id);
        }

        public int GetMeetingOwner(int id)
        {
            return _taskTemplateRepository.GetMeetingOwner(id);
        }

        public bool AddTagToMeeting(int id, string tagName)
        {
            return _taskTemplateRepository.AddTagToMeeting(id, tagName);
        }

        public bool RemoveTagFromMeeting(int id, int tagId)
        {
            return _taskTemplateRepository.RemoveTagFromMeeting(id, tagId);
        }

        public bool CreateMeeting(MeetingCreateApiModel apiModel, int createdUser)
        {
            var validator = _meetingValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _taskTemplateRepository.CreateMeeting(apiModel, createdUser);
            }
            return false;
        }

        public bool UpdateMeeting(int id, MeetingCreateApiModel apiModel, int modifiedUser)
        {
            var validator = _meetingValidator.Validate(apiModel);

            if (validator.IsValid)
            {
                return _taskTemplateRepository.UpdateMeeting(id, apiModel, modifiedUser);
            }
            return false;
        }

        public bool DeleteMeeting(int id)
        {
            return _taskTemplateRepository.DeleteMeeting(id);
        }

        public bool AddParticipantToMeeting(int id, MeetingParticipantCreateModel apiModel)
        {
            return _taskTemplateRepository.AddParticipantToMeeting(id, apiModel);
        }

        public bool RemoveParticipantFromMeeting(int id, MeetingParticipantCreateModel apiModel)
        {
            return _taskTemplateRepository.RemoveParticipantFromMeeting(id, apiModel);
        }

        public MeetingDetailApiModel GetMeeting(int id)
        {
            var dbMeeting = _taskTemplateRepository.GetMeeting(id);
            if(dbMeeting != null)
            {
                var apiModel = new MeetingDetailApiModel();

                if(dbMeeting.HostUser != null)
                {
                    apiModel.host = new UserLinkApiModel() { id = dbMeeting.HostUser.ID, username = dbMeeting.HostUser.Username, email = dbMeeting.HostUser.Email};
                }
                apiModel.id = dbMeeting.ID;
                apiModel.createdAt = dbMeeting.TASK_TEMPLATE.CreatedAt.GetValueOrDefault();
                apiModel.modifiedAt = dbMeeting.TASK_TEMPLATE.ModifiedAt.GetValueOrDefault();
                if(dbMeeting.TASK_TEMPLATE.CreatedUser != null)
                {
                    apiModel.createdBy = new UserLinkApiModel() { id = dbMeeting.TASK_TEMPLATE.CreatedUser.ID, username = dbMeeting.TASK_TEMPLATE.CreatedUser?.Username, email = dbMeeting.TASK_TEMPLATE.CreatedUser.Email };
                }

                if(dbMeeting.TASK_TEMPLATE.ModifiedUSer != null)
                {
                    apiModel.modifiedBy = new UserLinkApiModel() { id = dbMeeting.TASK_TEMPLATE.ModifiedUSer.ID, username = dbMeeting.TASK_TEMPLATE.ModifiedUSer?.Username, email = dbMeeting.TASK_TEMPLATE.ModifiedUSer.Email};
                }

                apiModel.priorities = _priorityRepository.GetAllPriorities().Select(c => new PrioritySelectionApiModel() { id = c.ID, name = c.Name, selected = dbMeeting.TASK_TEMPLATE.PRIORITY != null ? dbMeeting.TASK_TEMPLATE.PRIORITY.ID == c.ID : false }).ToList();

                apiModel.tags = dbMeeting.TAG_ITEM.Select(c => new TagApiModel() { id = c.TAG.ID, name = c.TAG.Name }).ToList();
                apiModel.notes = dbMeeting.TASK_TEMPLATE.NOTEs.Select(c => new NoteApiModel() { id = c.ID, avatar = $"{StaticStrings.ServerHost}avatar?fileName={c.USER.Avatar}", body = c.NoteBody, createdAt = c.CreatedAt.GetValueOrDefault(), createdBy = new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username, email = c.USER.Email }, files = c.FILEs.Select(f => new FileApiModel() { id = f.ID, fileName = f.FileName, size = f.FileSize.Value.ToString() + " KB", url = StaticStrings.ServerHost + "files/" + f.ID }).ToList() }).ToList();

                apiModel.description = dbMeeting.TASK_TEMPLATE.Description;
                apiModel.fromDate = dbMeeting.FromDate.GetValueOrDefault();
                apiModel.toDate = dbMeeting.ToDate.GetValueOrDefault();
                apiModel.title = dbMeeting.TASK_TEMPLATE.Title;
                apiModel.rrule = dbMeeting.TASK_TEMPLATE.RRule;
                apiModel.isAllDay = dbMeeting.IsAllDay.GetValueOrDefault();
                apiModel.isReminder = dbMeeting.TASK_TEMPLATE.IsRepeat.GetValueOrDefault();
                apiModel.location = dbMeeting.Location;

                var contactList = dbMeeting.MEETING_PARTICIPANT.Where(c => c.CONTACT != null);
                if(contactList.Count() > 0)
                {
                    apiModel.participants.contacts = contactList.Select(c => new ContactLinkApiModel() { id = c.CONTACT.ID, name = c.CONTACT.Name, email = c.CONTACT.Email }).ToList();
                }
                var leadList = dbMeeting.MEETING_PARTICIPANT.Where(c => c.LEAD != null);
                if(leadList.Count() > 0)
                {
                    apiModel.participants.leads = leadList.Select(c => new LeadLinkApiModel() { id = c.LEAD.ID, name = c.LEAD.Name, email = c.LEAD.Email }).ToList();
                }

                var userList = dbMeeting.MEETING_PARTICIPANT.Where(c => c.USER != null);
                if(userList.Count() > 0)
                {
                    apiModel.participants.users = userList.Select(c => new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username, email = c.USER.Email }).ToList();
                }

                //var calId = _googleCalendar.GetId(dbMeeting.HostUser.Email);
                //var eventId = dbMeeting.TASK_TEMPLATE.EventId;
                //apiModel.link = _googleCalendar.GetHtmlLink(calId, eventId);
                return apiModel;
            }
            else
            {
                return null;
            }
        }

        //get task template id
        public int GetTaskTemplateId(int id)
        {
            return _taskTemplateRepository.GetTaskTemplateId(id);
        }
        
        public int GetTaskOwner(int id)
        {
            return _taskTemplateRepository.GetTaskOwner(id);
        }

        public bool AddTagToTask(int id, string tagName)
        {
            return _taskTemplateRepository.AddTagToTask(id, tagName);
        }

        public bool RemoveTagFromTask(int id, int tagId)
        {
            return _taskTemplateRepository.RemoveTagFromTask(id, tagId);
        }

        public bool CreateTask(TaskCreateApiModel apiModel, int createdUser)
        {
            var validator = _taskValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _taskTemplateRepository.CreateTask(apiModel, createdUser);
            }
            return false;
        }

        public bool UpdateTask(int id, TaskCreateApiModel apiModel, int modifiedUser)
        {
            var validator = _taskValidator.Validate(apiModel);

            if (validator.IsValid)
            {
                return _taskTemplateRepository.UpdateTask(id, apiModel, modifiedUser);
            }
            return false;
        }

        public bool DeleteTask(int id)
        {
            return _taskTemplateRepository.DeleteTask(id);
        }

        public MeetingBlankApiModel PrepareNewMeeting()
        {
            var apiModel = new MeetingBlankApiModel();
            apiModel.priorities = _priorityRepository.GetAllPriorities().Select(c => new PrioritySelectionApiModel() { id = c.ID, name = c.Name, selected = false }).ToList();
            return apiModel;
        }

        public TaskBlankApiModel PrepareNewTask()
        {
            var apiModel = new TaskBlankApiModel();
            apiModel.priorities = _priorityRepository.GetAllPriorities().Select(c => new PrioritySelectionApiModel() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.statuses = _taskTemplateRepository.GetAllTaskStatuses().Select(c => new TaskStatus() { id = c.ID, name = c.Name, selected = false }).ToList();
            return apiModel;
        }

        public TaskDetailApiModel GetTask(int id)
        {
            var dbTask = _taskTemplateRepository.GetTask(id);
            if (dbTask != null)
            {
                var apiModel = new TaskDetailApiModel();
                apiModel.id = dbTask.ID;
                if (dbTask.CONTACT != null)
                {
                    apiModel.contact = new ContactLinkApiModel() { id = dbTask.CONTACT.ID, name = dbTask.CONTACT.Name, email = dbTask.CONTACT.Email };
                }
                if (dbTask.LEAD != null)
                {
                    apiModel.lead = new LeadLinkApiModel() { id = dbTask.LEAD.ID, name = dbTask.LEAD.Name, email = dbTask.LEAD.Email };

                }
                if (dbTask.ACCOUNT != null)
                {
                    apiModel.relatedAccount = new AccountLinkApiModel() { id = dbTask.ACCOUNT.ID, name = dbTask.ACCOUNT.Name, email = dbTask.ACCOUNT.Email };
                }
                if (dbTask.DEAL != null)
                {
                    apiModel.relatedDeal = new DealLinkApiModel() { id = dbTask.DEAL.ID, name = dbTask.DEAL.Name };
                }
                if (dbTask.CAMPAIGN != null)
                {
                    apiModel.relatedCampaign = new CampaignLinkApiModel() { id = dbTask.CAMPAIGN.ID, name = dbTask.CAMPAIGN.Name };
                }

                apiModel.priorities = _priorityRepository.GetAllPriorities().Select(c => new PrioritySelectionApiModel() { id = c.ID, name = c.Name, selected = dbTask.TASK_TEMPLATE.PRIORITY != null ? dbTask.TASK_TEMPLATE.PRIORITY.ID == c.ID : false }).ToList();

                apiModel.statuses = _taskTemplateRepository.GetAllTaskStatuses().Select(c => new TaskStatus() { id = c.ID, name = c.Name, selected = dbTask.TASK_TEMPLATE.TASK_STATUS != null ? c.ID == dbTask.TASK_TEMPLATE.TASK_STATUS.ID : false }).ToList();

                apiModel.title = dbTask.TASK_TEMPLATE.Title;
                apiModel.isRepeat = dbTask.TASK_TEMPLATE.IsRepeat.GetValueOrDefault();
                apiModel.rrule = dbTask.TASK_TEMPLATE.RRule;
                apiModel.description = dbTask.TASK_TEMPLATE.Description;

                if (dbTask.USER != null)
                {
                    apiModel.owner = new UserLinkApiModel() { id = dbTask.USER.ID, username = dbTask.USER.Username, email = dbTask.USER.Email };
                }
                apiModel.createdAt = dbTask.TASK_TEMPLATE.CreatedAt.GetValueOrDefault();
                apiModel.modifiedAt = dbTask.TASK_TEMPLATE.ModifiedAt.GetValueOrDefault();
                apiModel.createdBy = new UserLinkApiModel() { id = dbTask.TASK_TEMPLATE.CreatedUser.ID, username = dbTask.TASK_TEMPLATE.CreatedUser.Username, email = dbTask.TASK_TEMPLATE.CreatedUser.Email };
                if (dbTask.TASK_TEMPLATE.ModifiedUSer != null)
                {
                    apiModel.modifiedBy = new UserLinkApiModel() { id = dbTask.TASK_TEMPLATE.ModifiedUSer.ID, username = dbTask.TASK_TEMPLATE.ModifiedUSer.Username, email = dbTask.TASK_TEMPLATE.ModifiedUSer.Email };

                }
                if (dbTask.TASK_TEMPLATE.StartDate != null)
                {
                    apiModel.startDate = dbTask.TASK_TEMPLATE.StartDate.Value;
                }
                if (dbTask.TASK_TEMPLATE.DueDate != null)
                {
                    apiModel.dueDate = dbTask.TASK_TEMPLATE.DueDate.Value;
                }

                apiModel.tags = dbTask.TAG_ITEM.Select(c => new TagApiModel() { id = c.TAG.ID, name = c.TAG.Name }).ToList();
                apiModel.notes = dbTask.TASK_TEMPLATE.NOTEs.Select(c => new NoteApiModel() { id = c.ID, avatar = $"{StaticStrings.ServerHost}avatar?fileName={c.USER.Avatar}", body = c.NoteBody, createdAt = c.CreatedAt.GetValueOrDefault(), createdBy = new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username, email = c.USER.Email }, files = c.FILEs.Select(f => new FileApiModel() { id = f.ID, fileName = f.FileName, size = f.FileSize.Value.ToString() + " KB", url = StaticStrings.ServerHost + "files/" + f.ID }).ToList() }).ToList();

                var calId = dbTask.USER.CalendarId;
                var eventId = dbTask.TASK_TEMPLATE.EventId;
                apiModel.link = googleCalendar.GetHtmlLink(calId, eventId);
                return apiModel;
            }
            else
            {
                return null;
            }
        }
    }
}