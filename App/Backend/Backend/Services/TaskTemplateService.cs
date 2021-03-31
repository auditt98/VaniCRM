using Backend.Domain;
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

        public List<TASK_TEMPLATE> GetUserTaskTemplate(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            return _taskTemplateRepository.GetUserTaskTemplate(userID, q, currentPage, pageSize).ToList();
        }

        public CallBlankApiModel PrepareNewCall()
        {
            var apiModel = new CallBlankApiModel();

            apiModel.statuses = _taskTemplateRepository.GetAllTaskStatuses().Select(c => new TaskStatus() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.types = _taskTemplateRepository.GetAllCallTypes().Select(c => new CallType() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.results = _taskTemplateRepository.GetAllCallResults().Select(c => new CallResult() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.purposes = _taskTemplateRepository.GetAllCallReasons().Select(c => new CallPurpose() { id = c.ID, name = c.Name, selected = false }).ToList();


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
                if(dbCall.CONTACT != null)
                {
                    apiModel.contact = new ContactLinkApiModel() { id = dbCall.CONTACT.ID, name = dbCall.CONTACT.Name };
                }
                if(dbCall.LEAD != null)
                {
                    apiModel.lead = new LeadLinkApiModel() { id = dbCall.LEAD.ID, name = dbCall.LEAD.Name };

                }
                if(dbCall.ACCOUNT != null)
                {
                    apiModel.relatedAccount = new AccountLinkApiModel() { id = dbCall.ACCOUNT.ID, name = dbCall.ACCOUNT.Name };
                }
                if(dbCall.DEAL != null)
                {
                    apiModel.relatedDeal = new DealLinkApiModel() { id = dbCall.DEAL.ID, name = dbCall.DEAL.Name };
                }
                if(dbCall.CAMPAIGN != null)
                {
                    apiModel.relatedCampaign = new CampaignLinkApiModel() { id = dbCall.CAMPAIGN.ID, name = dbCall.CAMPAIGN.Name };
                }


                apiModel.purposes = _taskTemplateRepository.GetAllCallReasons().Select(c => new CallPurpose() { id = c.ID, name = c.Name, selected = c.ID == dbCall.CALL_REASON.ID }).ToList();
                apiModel.types = _taskTemplateRepository.GetAllCallTypes().Select(c => new CallType() { id = c.ID, name = c.Name, selected = c.ID == dbCall.CALL_TYPE.ID }).ToList();
                apiModel.statuses = _taskTemplateRepository.GetAllTaskStatuses().Select(c => new TaskStatus() { id = c.ID, name = c.Name, selected = c.ID == dbCall.TASK_TEMPLATE.TASK_STATUS.ID }).ToList();
                apiModel.results = _taskTemplateRepository.GetAllCallResults().Select(c => new CallResult() { id = c.ID, name = c.Name, selected = c.ID == dbCall.CALL_RESULT.ID }).ToList();

                apiModel.title = dbCall.TASK_TEMPLATE.Title;
                apiModel.duration = dbCall.Length.GetValueOrDefault();
                apiModel.startTime = dbCall.StartTime.GetValueOrDefault();
                apiModel.isRepeat = dbCall.TASK_TEMPLATE.IsRepeat.GetValueOrDefault();
                apiModel.rrule = dbCall.TASK_TEMPLATE.RRule;
                apiModel.description = dbCall.TASK_TEMPLATE.Description;

                if(dbCall.Owner != null)
                {
                    apiModel.owner = new UserLinkApiModel() { id = dbCall.Owner.ID, username = dbCall.Owner.Username };
                }
                apiModel.createdAt = dbCall.TASK_TEMPLATE.CreatedAt.GetValueOrDefault();
                apiModel.modifiedAt = dbCall.TASK_TEMPLATE.ModifiedAt.GetValueOrDefault();
                apiModel.createdBy = new UserLinkApiModel() { id = dbCall.TASK_TEMPLATE.CreatedUser.ID, username = dbCall.TASK_TEMPLATE.CreatedUser.Username };
                if(dbCall.TASK_TEMPLATE.ModifiedUSer != null)
                {
                    apiModel.modifiedBy = new UserLinkApiModel() { id = dbCall.TASK_TEMPLATE.ModifiedUSer.ID, username = dbCall.TASK_TEMPLATE.ModifiedUSer.Username };

                }
                apiModel.tags = dbCall.TAG_ITEM.Select(c => new TagApiModel() { id = c.TAG.ID, name = c.TAG.Name }).ToList();
                apiModel.notes = dbCall.TASK_TEMPLATE.NOTEs.Select(c => new NoteApiModel() { id = c.ID, avatar = $"{StaticStrings.ServerHost}avatar?fileName={dbCall.Owner.Avatar}", body = c.NoteBody, createdAt = c.CreatedAt.GetValueOrDefault(), createdBy = new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username }, files = c.FILEs.Select(f => new FileApiModel() { id = f.ID, fileName = f.FileName, size = f.FileSize.Value.ToString() + " KB", url = StaticStrings.ServerHost + "files/" + f.ID }).ToList() }).ToList();
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

        public MeetingDetailApiModel GetMeeting(int id)
        {
            var dbMeeting = _taskTemplateRepository.GetMeeting(id);
            if(dbMeeting != null)
            {
                var apiModel = new MeetingDetailApiModel();


                apiModel.host = new UserLinkApiModel() { id = dbMeeting.HostUser.ID, username = dbMeeting.HostUser.Username };
                apiModel.createdAt = dbMeeting.TASK_TEMPLATE.CreatedAt.GetValueOrDefault();
                apiModel.modifiedAt = dbMeeting.TASK_TEMPLATE.ModifiedAt.GetValueOrDefault();
                apiModel.createdBy = new UserLinkApiModel() { id = dbMeeting.TASK_TEMPLATE.CreatedUser.ID, username = dbMeeting.TASK_TEMPLATE.CreatedUser?.Username };
                apiModel.modifiedBy = new UserLinkApiModel() { id = dbMeeting.TASK_TEMPLATE.ID, username = dbMeeting.TASK_TEMPLATE.ModifiedUSer?.Username };
                apiModel.tags = dbMeeting.TAG_ITEM.Select(c => new TagApiModel() { id = c.TAG.ID, name = c.TAG.Name }).ToList();
                apiModel.notes = dbMeeting.TASK_TEMPLATE.NOTEs.Select(c => new NoteApiModel() { id = c.ID, avatar = $"{StaticStrings.ServerHost}avatar?fileName={dbMeeting.HostUser.Avatar}", body = c.NoteBody, createdAt = c.CreatedAt.GetValueOrDefault(), createdBy = new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username }, files = c.FILEs.Select(f => new FileApiModel() { id = f.ID, fileName = f.FileName, size = f.FileSize.Value.ToString() + " KB", url = StaticStrings.ServerHost + "files/" + f.ID }).ToList() }).ToList();
                return apiModel;
            }
            else
            {
                return null;
            }
        }
    }
}