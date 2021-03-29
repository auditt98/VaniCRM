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
                apiModel.contact = new ContactLinkApiModel() { id = dbCall.CONTACT.ID, name = dbCall.CONTACT.Name };
                apiModel.lead = new LeadLinkApiModel() { id = dbCall.LEAD.ID, name = dbCall.LEAD.Name };
                apiModel.relatedAccount = new AccountLinkApiModel() { id = dbCall.ACCOUNT.ID, name = dbCall.ACCOUNT.Name };
                apiModel.relatedDeal = new DealLinkApiModel() { id = dbCall.DEAL.ID, name = dbCall.DEAL.Name };
                apiModel.relatedCampaign = new CampaignLinkApiModel() { id = dbCall.CAMPAIGN.ID, name = dbCall.CAMPAIGN.Name };

                apiModel.purposes = _taskTemplateRepository.GetAllCallReasons().Select(c => new CallPurpose() { id = c.ID, name = c.Name, selected = c.ID == dbCall.CALL_REASON.ID }).ToList();
                apiModel.types = _taskTemplateRepository.GetAllCallTypes().Select(c => new CallType() { id = c.ID, name = c.Name, selected = c.ID == dbCall.CALL_TYPE.ID }).ToList();
                apiModel.statuses = _taskTemplateRepository.GetAllTaskStatuses().Select(c => new TaskStatus() { id = c.ID, name = c.Name, selected = c.ID == dbCall.TASK_TEMPLATE.TASK_STATUS.ID }).ToList();
                apiModel.results = _taskTemplateRepository.GetAllCallResults().Select(c => new CallResult() { id = c.ID, name = c.Name, selected = c.ID == dbCall.CALL_RESULT.ID }).ToList();

                apiModel.title = dbCall.TASK_TEMPLATE.Title;
                apiModel.duration = dbCall.Length.GetValueOrDefault();
                apiModel.startTime = dbCall.StartTime.GetValueOrDefault();
                apiModel.isReminder = dbCall.TASK_TEMPLATE.IsRepeat.GetValueOrDefault();
                apiModel.rrule = dbCall.TASK_TEMPLATE.RRule;
                apiModel.description = dbCall.TASK_TEMPLATE.Description;

                apiModel.owner = new UserLinkApiModel() { id = dbCall.Owner.ID, username = dbCall.Owner.Username };
                apiModel.createdAt = dbCall.TASK_TEMPLATE.CreatedAt.Value;
                apiModel.modifiedAt = dbCall.TASK_TEMPLATE.ModifiedAt.Value;
                apiModel.createdBy = new UserLinkApiModel() { id = dbCall.TASK_TEMPLATE.CreatedUser.ID, username = dbCall.TASK_TEMPLATE.CreatedUser.Username };
                apiModel.modifiedBy = new UserLinkApiModel() { id = dbCall.TASK_TEMPLATE.ID, username = dbCall.TASK_TEMPLATE.ModifiedUSer.Username };
                apiModel.tags = dbCall.TAG_ITEM.Select(c => new TagApiModel() { id = c.TAG.ID, name = c.TAG.Name }).ToList();
                apiModel.notes = dbCall.TASK_TEMPLATE.NOTEs.Select(c => new NoteApiModel() { id = c.ID, body = c.NoteBody, createdAt = c.CreatedAt.Value, createdBy = new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username }, files = c.FILEs.Select(f => new FileApiModel() { id = f.ID, fileName = f.FileName, size = f.FileSize.Value.ToString() + " KB", url = StaticStrings.ServerHost + "files/" + f.ID }).ToList() }).ToList();
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

    }
}