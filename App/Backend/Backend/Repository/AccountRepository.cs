using Backend.Domain;
using Backend.Extensions;
using Backend.Models.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class AccountRepository
    {
        DatabaseContext db = new DatabaseContext();
        TagRepository _tagRepository = new TagRepository();
        ContactRepository _contactRepository = new ContactRepository();

        public IEnumerable<ACCOUNT> GetUserAccounts(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            var dbUser = db.USERs.Find(userID);
            var accounts = dbUser.AccountsAsOwner.ToList();
            accounts.AddRange(dbUser.AccountsAsCollaborator);

            if (pageSize == 0)
            {
                pageSize = accounts.Count();
            }

            if (String.IsNullOrEmpty(q))
            {
                return accounts.OrderBy(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize);
            }
            var result = accounts.OrderBy(c=>c.ID).Where(c => c.Name.ToLower().Contains(q.ToLower()) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q.ToLower()) || c.TaxCode.ToLower().Contains(q.ToLower())).Skip((currentPage - 1) * pageSize).Take(pageSize);

            return result;
        }

        public (IEnumerable<ACCOUNT> accounts, Pager p) GetAllAccounts(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            if (pageSize == 0)
            {
                pageSize = 10;
            }

            if (String.IsNullOrEmpty(q))
            {
                Pager pager = new Pager(db.ACCOUNTs.Count(), currentPage, pageSize, 9999);
                return (db.ACCOUNTs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
            }
            var accounts = db.ACCOUNTs.Where(c => c.Name.ToLower().Contains(q) || c.Phone.Contains(q)).OrderBy(c => c.ID);
            if (accounts.Count() > 0)
            {
                Pager p = new Pager(accounts.Count(), currentPage, pageSize, 9999);

                return (accounts.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
            }
            else
            {
                return (accounts, null);
            }
        }

        public bool Create(AccountCreateApiModel apiModel, int createdUser)
        {
            var newAccount = new ACCOUNT();
            newAccount.AccountOwner = apiModel.owner != 0 ? apiModel.owner : createdUser;
            newAccount.Name = apiModel.name;
            newAccount.Email = apiModel.email;
            newAccount.Phone = apiModel.phone;
            newAccount.Fax = apiModel.fax;
            newAccount.TaxCode = apiModel.taxCode;
            newAccount.NoEmployees = apiModel.numberOfEmployees;
            newAccount.AnnualRevenue = apiModel.annualRevenue;
            newAccount.Website = apiModel.website;
            newAccount.BankName = apiModel.bankName;
            newAccount.BankAccountName = apiModel.bankAccountName;
            newAccount.BankAccount = apiModel.bankAccount;
            newAccount.Country = apiModel.country;
            newAccount.City = apiModel.city;
            newAccount.AddressDetail = apiModel.addressDetail;
            newAccount.CreatedAt = DateTime.Now;
            newAccount.CreatedBy = createdUser;
            if(apiModel.collaborator != 0)
            {
                newAccount.AccountCollaborator = apiModel.collaborator;
            }
            try
            {
                db.ACCOUNTs.Add(newAccount);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(int accountId, AccountCreateApiModel apiModel, int modifiedUser)
        {
            var dbAccount = db.ACCOUNTs.Find(accountId);
            if (dbAccount != null)
            {
                if(apiModel.owner != 0)
                {
                    dbAccount.AccountOwner = apiModel.owner;
                }
                dbAccount.Name = apiModel.name;
                dbAccount.Email = apiModel.email;
                dbAccount.Phone = apiModel.phone;
                dbAccount.Fax = apiModel.fax;
                dbAccount.TaxCode = apiModel.taxCode;
                dbAccount.NoEmployees = apiModel.numberOfEmployees;
                dbAccount.AnnualRevenue = apiModel.annualRevenue;
                dbAccount.Website = apiModel.website;
                dbAccount.BankName = apiModel.bankName;
                dbAccount.BankAccountName = apiModel.bankAccountName;
                dbAccount.BankAccount = apiModel.bankAccount;
                dbAccount.Country = apiModel.country;
                dbAccount.City = apiModel.city;
                dbAccount.AddressDetail = apiModel.addressDetail;
                dbAccount.ModifiedAt = DateTime.Now;
                dbAccount.ModifiedBy = modifiedUser;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    
        public int GetOwner(int id)
        {
            var dbAccount = db.ACCOUNTs.Find(id);
            if(dbAccount != null)
            {
                if(dbAccount.Owner != null)
                {
                    return dbAccount.Owner.ID;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public int GetCollaborator(int id)
        {
            var dbAccount = db.ACCOUNTs.Find(id);
            if (dbAccount != null)
            {
                if (dbAccount.Collaborator != null)
                {
                    return dbAccount.Collaborator.ID;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool Delete(int id)
        {
            var dbAccount = db.ACCOUNTs.Find(id);
            if (dbAccount != null)
            {
                db.ACCOUNTs.Remove(dbAccount);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChangeAvatar(int id, string fileName)
        {
            var dbAccount = db.ACCOUNTs.Find(id);
            if (dbAccount != null)
            {
                dbAccount.Avatar = fileName;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddTag(int id, string tagName)
        {
            var dbAccount = db.ACCOUNTs.Find(id);
            var dbTag = _tagRepository.GetOneByName(tagName);
            if (dbAccount != null)
            {
                if (dbTag != null)
                {
                    var tagItem = dbAccount.TAG_ITEM.Where(c => c.TAG_ID == dbTag.ID).FirstOrDefault();
                    if (tagItem != null)
                    {
                        var newTagItem = new TAG_ITEM();
                        newTagItem.TAG_ID = dbTag.ID;
                        newTagItem.ACCOUNT_ID = dbAccount.ID;
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
                    tagItem.ACCOUNT_ID = dbAccount.ID;
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

        public bool RemoveTag(int id, int tagId)
        {
            var dbAccount = db.ACCOUNTs.Find(id);
            if (dbAccount != null)
            {
                var tagItem = dbAccount.TAG_ITEM.Where(c => c.TAG.ID == tagId).FirstOrDefault();
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
        
        public ACCOUNT GetOne(int id)
        {
            return db.ACCOUNTs.Find(id);
        }

        public (IEnumerable<CONTACT> contacts, Pager p) GetContacts(int accountId, int currentPage, int pageSize, string query)
        {
            var dbAccount = db.ACCOUNTs.Find(accountId);
            if (dbAccount != null)
            {
                var q = query.ToLower();
                var contactList = dbAccount.CONTACTs;
                if (pageSize == 0)
                {
                    pageSize = 10;
                }

                if (String.IsNullOrEmpty(q))
                {
                    Pager pager = new Pager(contactList.Count(), currentPage, pageSize, 9999);
                    return (contactList.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
                }
                var filteredContactList = contactList.Where(c => c.Name.ToLower().Contains(q) || c.Email.ToLower().Contains(q) || c.Phone.Contains(q)).OrderBy(c => c.ID);
                if (filteredContactList.Count() > 0)
                {
                    Pager p = new Pager(filteredContactList.Count(), currentPage, pageSize, 9999);

                    return (filteredContactList.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
                }
                else
                {
                    return (filteredContactList, null);
                }
            }
            else
            {
                return (null, null);
            }
        }

        public bool AddContact(int accountId, int contactId)
        {
            var dbAccount = db.ACCOUNTs.Find(accountId);
            var dbContact = _contactRepository.GetOne(contactId);
            if (dbAccount != null && dbContact != null)
            {
                dbAccount.CONTACTs.Add(dbContact);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}