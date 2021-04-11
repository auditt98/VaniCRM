using Backend.Domain;
using Backend.Extensions;
using Backend.Models.ApiModel;
using Backend.Repository;
using Backend.Resources;
using Backend.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Backend.Services
{
    public class LeadService
    {
        LeadRepository _leadRepository = new LeadRepository();
        LeadValidator _leadValidator = new LeadValidator();
        IndustryRepository _industryRepository = new IndustryRepository();
        PriorityRepository _priorityRepository = new PriorityRepository();
        TagRepository _tagRepository = new TagRepository();
        TagService _tagService = new TagService();

        public LeadListApiModel GetLeadList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbLeads = _leadRepository.GetAllLeads(query, pageSize, currentPage);
            var apiModel = new LeadListApiModel();

            apiModel.leads = dbLeads.leads.Select(c => new LeadListApiModel.LeadInfo() { id = c.ID, name = c.Name, companyName = c.CompanyName, email = c.Email, phone = c.Phone, leadSource = c.LEAD_SOURCE.Name, leadOwner = c.Owner.FirstName + " " + c.Owner.LastName, priority = c.PRIORITY.Name }).ToList();
            apiModel.pageInfo = dbLeads.p;
            return apiModel;
        }

        public List<LEAD> GetUserLeads(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            return _leadRepository.GetUserLeads(userID, q, currentPage, pageSize).ToList();
        }

        public LeadBlankApiModel PrepareNewLead()
        {
            var apiModel = new LeadBlankApiModel();
            apiModel.industry = _industryRepository.GetAllIndustries().Select(c => new IndustrySelectionApiModel() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.leadSource = _leadRepository.GetAllLeadSources().Select(c => new LeadSource() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.priority = _priorityRepository.GetAllPriorities().Select(c => new PrioritySelectionApiModel() { id = c.ID, name = c.Name, selected = false }).ToList();
            apiModel.status = _leadRepository.GetAllLeadStatuses().Select(c => new LeadStatus() { id = c.ID, name = c.Name, selected = false }).ToList();
            return apiModel;
        }

        public bool Create(LeadCreateApiModel apiModel, int createdUser)
        {
            var validator = _leadValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _leadRepository.Create(apiModel, createdUser);
            }
            return false;
        }

        public bool Update(int id, LeadCreateApiModel apiModel, int modifiedUser)
        {
            var validator = _leadValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _leadRepository.Update(id, apiModel, modifiedUser);
            }
            return false;
        }

        public int FindOwnerId(int leadId)
        {
            var dbLead = _leadRepository.GetOne(leadId);
            if(dbLead != null)
            {
                return dbLead.Owner.ID;
            }
            else { return 0; }
        }

        public bool ChangeAvatar(int id, HttpPostedFile uploadedFile)
        {
            FileManager.File file = new FileManager.File(uploadedFile);
            var isImage = file.FilterExtension(new List<string>() { ".jpeg", ".jpg", ".png", ".tif", ".tiff" });
            if (isImage)
            {
                file.Rename();
                var isChanged = _leadRepository.ChangeAvatar(id, file.FullName);
                if (isChanged)
                {
                    file.Save(HttpContext.Current.Server.MapPath("~/Uploads"));
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

        public bool Delete(int id)
        {
            return _leadRepository.Delete(id);
        }
    
        public LeadDetailApiModel GetOne(int id)
        {
            string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
            var dbLead = _leadRepository.GetOne(id);
            if(dbLead != null)
            {
                var apiModel = new LeadDetailApiModel();
                apiModel.addressDetail = dbLead.AddressDetail;
                apiModel.annualRevenue = dbLead.AnnualRevenue.GetValueOrDefault();
                //avatar
                if (dbLead.Avatar != null)
                {
                    //var mime = MimeMapping.MimeUtility.GetMimeMapping(dbLead.Avatar);
                    //var img = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(targetFolder, dbLead.Avatar)));
                    apiModel.avatar = $"{StaticStrings.ServerHost}avatar?fileName={dbLead.Avatar}";
                }

                apiModel.city = dbLead.City;
                apiModel.companyName = dbLead.CompanyName;
                apiModel.country = dbLead.Country;
                apiModel.CreatedAt = dbLead.CreatedAt.GetValueOrDefault();
                apiModel.CreatedBy = new UserLinkApiModel() { id = dbLead.CreatedUser.ID, username = dbLead.CreatedUser.Username, email = dbLead.CreatedUser.Email };
                apiModel.description = dbLead.Description;
                apiModel.email = dbLead.Email;
                apiModel.fax = dbLead.Fax;
                apiModel.id = dbLead.ID;
                apiModel.industry = _industryRepository.GetAllIndustries().Select(c => new IndustrySelectionApiModel() { id = c.ID, name = c.Name, selected = dbLead.INDUSTRY.ID == c.ID }).ToList();
                apiModel.leadSource = _leadRepository.GetAllLeadSources().Select(c => new LeadSource() { id = c.ID, name = c.Name, selected = dbLead.LEAD_SOURCE != null ? dbLead.LEAD_SOURCE.ID == c.ID : false}).ToList();
                apiModel.ModifiedAt = dbLead.ModifiedAt.GetValueOrDefault();
                apiModel.ModifiedBy = new UserLinkApiModel() { id = dbLead.ModifiedUser?.ID, username = dbLead.ModifiedUser?.Username, email = dbLead.ModifiedUser?.Email };
                apiModel.name = dbLead.Name;
                apiModel.noCall = dbLead.NoCall.GetValueOrDefault();
                apiModel.noEmail = dbLead.NoEmail.GetValueOrDefault();
                apiModel.owner = new UserLinkApiModel() { id = dbLead.Owner?.ID, username = dbLead.Owner?.Username, email = dbLead.Owner?.Email };
                //notes
                apiModel.notes = dbLead.NOTEs.Select(c => new NoteApiModel() { id = c.ID, avatar = $"{StaticStrings.ServerHost}avatar?fileName={c.USER?.Avatar}", body = c.NoteBody, createdAt = c.CreatedAt.GetValueOrDefault(), createdBy = new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username, email = c.USER.Email}, files = c.FILEs.Select( f => new FileApiModel() { id = f.ID, fileName = f.FileName, size = f.FileSize.Value.ToString() + " KB", url = StaticStrings.ServerHost + "files/" + f.ID}).ToList()}).ToList();
                apiModel.phone = dbLead.Phone;
                apiModel.priority = _priorityRepository.GetAllPriorities().Select(c => new PrioritySelectionApiModel() { id = c.ID, name = c.Name, selected = dbLead.PRIORITY != null ? dbLead.PRIORITY.ID == c.ID : false }).ToList();
                apiModel.skype = dbLead.Skype;
                apiModel.status = _leadRepository.GetAllLeadStatuses().Select(c => new LeadStatus() { id = c.ID, name = c.Name, selected = dbLead.Status != null ? c.ID == dbLead.Status.ID : false}).ToList();
                //tags
                apiModel.tags = dbLead.TAG_ITEM.Select(c => new TagApiModel() { id = c.TAG.ID, name = c.TAG.Name }).ToList();
                apiModel.website = dbLead.Website;
                return apiModel;
            }
            else
            {
                return null;
            }
        }
    
        public bool AddTag(int id, string tagName)
        {
            return _leadRepository.AddTag(id, tagName);
        }

        public bool RemoveTag(int id, int tagId)
        {
            return _leadRepository.RemoveTag(id, tagId);
        }

        public LeadConvertApiModel Convert(int id, int convertUser)
        {
            return _leadRepository.Convert(id, convertUser);
        }
    }
}