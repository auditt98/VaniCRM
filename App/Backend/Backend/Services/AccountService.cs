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
    public class AccountService
    {
        AccountRepository _accountRepository = new AccountRepository();
        PriorityRepository _priorityRepository = new PriorityRepository();
        AccountValidator _accountValidator = new AccountValidator();

        public List<ACCOUNT> GetUserAccounts(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            return _accountRepository.GetUserAccounts(userID, q, currentPage, pageSize).ToList();

        }

        public List<AccountListApiModel.AccountInfo> GetAccountList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbAccounts = _accountRepository.GetAllAccounts(query, pageSize, currentPage);
            return dbAccounts.Select(c => new AccountListApiModel.AccountInfo() { id = c.ID, name = c.Name, website = c.Website, phone = c.Phone, owner = c.Owner.FirstName + " " + c.Owner.LastName }).ToList();
        }

       
        public bool Create(AccountCreateApiModel apiModel, int createdUser)
        {
            var validator = _accountValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _accountRepository.Create(apiModel, createdUser);
            }
            return false;
        }

        public bool Update(int id, AccountCreateApiModel apiModel, int modifiedUser)
        {
            var validator = _accountValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _accountRepository.Update(id, apiModel, modifiedUser);
            }
            return false;
        }

        public int FindOwnerId(int accountId)
        {
            return _accountRepository.GetOwner(accountId);
        }

        public int FindCollaboratorId(int accountId)
        {
            return _accountRepository.GetCollaborator(accountId);
        }

        public bool Delete(int id)
        {
            return _accountRepository.Delete(id);
        }

        public bool ChangeAvatar(int id, HttpPostedFile uploadedFile)
        {
            FileManager.File file = new FileManager.File(uploadedFile);
            var isImage = file.FilterExtension(new List<string>() { ".jpeg", ".jpg", ".png", ".tif", ".tiff" });
            if (isImage)
            {
                file.Rename();
                var isChanged = _accountRepository.ChangeAvatar(id, file.FullName);
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

        public bool AddTag(int id, string tagName)
        {
            return _accountRepository.AddTag(id, tagName);
        }

        public bool RemoveTag(int id, int tagId)
        {
            return _accountRepository.RemoveTag(id, tagId);
        }

        public AccountDetailApiModel GetOne(int id)
        {
            string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
            var dbAccount = _accountRepository.GetOne(id);
            if (dbAccount != null)
            {
                var apiModel = new AccountDetailApiModel();

                apiModel.id = dbAccount.ID;
                //avatar
                if (dbAccount.Avatar != null)
                {
                    //var mime = MimeMapping.MimeUtility.GetMimeMapping(dbLead.Avatar);
                    //var img = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(targetFolder, dbLead.Avatar)));
                    apiModel.avatar = $"{StaticStrings.ServerHost}avatar?fileName={dbAccount.Avatar}";
                }

                apiModel.name = dbAccount.Name;
                apiModel.email = dbAccount.Email;
                apiModel.phone = dbAccount.Phone;
                apiModel.fax = dbAccount.Fax;
                apiModel.taxCode = dbAccount.TaxCode;
                apiModel.numberOfEmployees = dbAccount.NoEmployees.GetValueOrDefault();
                apiModel.annualRevenue = dbAccount.AnnualRevenue.GetValueOrDefault();
                apiModel.website = dbAccount.Website;
                apiModel.bankName = dbAccount.BankName;
                apiModel.bankAccountName = dbAccount.BankAccountName;
                apiModel.bankAccount = dbAccount.BankAccount;
                apiModel.country = dbAccount.Country;
                apiModel.city = dbAccount.City;
                apiModel.addressDetail = dbAccount.AddressDetail;

                apiModel.CreatedAt = dbAccount.CreatedAt.GetValueOrDefault();
                apiModel.CreatedBy = new UserLinkApiModel() { id = dbAccount.CreatedUser.ID, username = dbAccount.CreatedUser.Username, email = dbAccount.CreatedUser.Email };
                apiModel.ModifiedAt = dbAccount.ModifiedAt.GetValueOrDefault();
                apiModel.ModifiedBy = new UserLinkApiModel() { id = dbAccount.ModifiedUser?.ID, username = dbAccount.ModifiedUser?.Username, email = dbAccount.ModifiedUser?.Email };

                apiModel.owner = new UserLinkApiModel() { id = dbAccount.Owner?.ID, username = dbAccount.Owner?.Username, email = dbAccount.Owner?.Email };
                apiModel.collaborator = new UserLinkApiModel() { id = dbAccount.Collaborator?.ID, username = dbAccount.Collaborator?.Username, email = dbAccount.Collaborator?.Email };

                if(dbAccount.LEAD != null)
                {
                    apiModel.convertedFrom = new LeadLinkApiModel() { id = dbAccount.LEAD.ID, email = dbAccount.LEAD.Email, name = dbAccount.LEAD.Name };
                }
                //notes
                apiModel.notes = dbAccount.NOTEs.Select(c => new NoteApiModel() { id = c.ID, avatar = $"{StaticStrings.ServerHost}avatar?fileName={c.USER?.Avatar}", body = c.NoteBody, createdAt = c.CreatedAt.GetValueOrDefault(), createdBy = new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username, email = c.USER.Email }, files = c.FILEs.Select(f => new FileApiModel() { id = f.ID, fileName = f.FileName, size = f.FileSize.Value.ToString() + " KB", url = StaticStrings.ServerHost + "files/" + f.ID }).ToList() }).ToList();

                //tags
                apiModel.tags = dbAccount.TAG_ITEM.Select(c => new TagApiModel() { id = c.TAG.ID, name = c.TAG.Name }).ToList();
                return apiModel;
            }
            else
            {
                return null;
            }
        }

        public ContactListApiModel GetContacts(int accountId, int currentPage, int pageSize, string query)
        {
            var dbContacts = _accountRepository.GetContacts(accountId, currentPage, pageSize, query);
            var apiModel = new ContactListApiModel();
            apiModel.contacts = dbContacts.contacts.Select(c => new ContactListApiModel.ContactInfo() { id = c.ID, contactName = c.Name, accountName = c.ACCOUNT.Name, email = c.Email, phone = c.Phone, owner = c.Owner.FirstName + " " + c.Owner.LastName }).ToList();
            apiModel.pageInfo = dbContacts.p;
            return apiModel;
        }

        public bool AddContact(int accountId, int contactId)
        {
            return _accountRepository.AddContact(accountId, contactId);
        }
    }


}