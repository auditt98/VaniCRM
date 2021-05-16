using Backend.Domain;
using Backend.Extensions;
using Backend.Models.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Repository
{
    public class ContactRepository
    {
        DatabaseContext db = new DatabaseContext();
        TagRepository _tagRepository = new TagRepository();


        public IEnumerable<CONTACT> GetUserContacts(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            var dbUser = db.USERs.Find(userID);
            var contacts = dbUser.ContactsAsOwner.ToList();
            contacts.AddRange(dbUser.ContactsAsCollaborator);

            if (pageSize == 0)
            {
                pageSize = contacts.Count();
            }

            if (String.IsNullOrEmpty(q))
            {
                return contacts.OrderBy(c=>c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            }
            var result = contacts.Where(c => c.Name.ToLower().Contains(q.ToLower()) || c.Phone.Contains(q) || c.Email.ToLower().Contains(q.ToLower()) || c.Mobile.Contains(q)).OrderByDescending(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();


            return result;
        }

        public (IEnumerable<CONTACT> contacts, Pager p) GetAllContacts(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var q = query.ToLower();
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            if (String.IsNullOrEmpty(q))
            {
                Pager pager = new Pager(db.CONTACTs.Count(), currentPage, pageSize, 9999);
                return (db.CONTACTs.OrderByDescending(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
            }
            var contacts = db.CONTACTs.Where(c => c.Name.ToLower().Contains(q) || c.ACCOUNT.Name.ToLower().Contains(q) || c.Email.ToLower().Contains(q) || c.Phone.Contains(q)).OrderByDescending(c => c.ID);
            if (contacts.Count() > 0)
            {
                Pager p = new Pager(contacts.Count(), currentPage, pageSize, 9999);

                return (contacts.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
            }
            else
            {
                return (contacts, null);
            }
        }

        public CONTACT GetOne(int id)
        {
            return db.CONTACTs.Find(id);
        }

        public bool Create(ContactCreateApiModel apiModel, int createdUser)
        {
            var newContact = new CONTACT();

            newContact.ContactOwner = apiModel.owner != 0 ? apiModel.owner : createdUser;

            if(apiModel.collaborator != 0)
            {
                newContact.ContactCollaborator = apiModel.collaborator;
            }
            newContact.Name = apiModel.name;
            newContact.Email = apiModel.email;
            newContact.Phone = apiModel.phone;
            newContact.Mobile = apiModel.mobile;
            newContact.DepartmentName = apiModel.departmentName;
            newContact.Birthday = apiModel.birthday;

            if(apiModel.account != 0)
            {
                newContact.ACCOUNT_ID = apiModel.account;
            }

            if(apiModel.priority != 0)
            {
                newContact.PRIORITY_ID= apiModel.priority;

            }
            newContact.NoEmail = apiModel.noEmail;
            newContact.NoCall = apiModel.noCall;
            newContact.Skype = apiModel.skype;
            newContact.AssistantName = apiModel.assistantName;
            newContact.AssistantPhone = apiModel.assistantPhone;

            newContact.Country = apiModel.country;
            newContact.City = apiModel.city;
            newContact.AddressDetail = apiModel.addressDetail;
            newContact.CreatedAt = DateTime.Now;
            newContact.CreatedBy = createdUser;
            newContact.ModifiedAt = DateTime.Now;
            try
            {
                db.CONTACTs.Add(newContact);
                db.SaveChanges();
                var owner = db.USERs.Find(newContact.ContactOwner);
                var collaborator = db.USERs.Find(newContact.ContactCollaborator);
                var creator = db.USERs.Find(createdUser);
                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Contact assigned";
                notifyModel.content = $"Contact {newContact.Name} has been created and assigned to you by {creator?.Username}.";
                notifyModel.createdAt = DateTime.Now;
                notifyModel.module = "contacts";
                notifyModel.moduleObjectId = newContact.ID;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, collaborator });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetOwner(int id)
        {
            var dbContact = db.CONTACTs.Find(id);
            if (dbContact != null)
            {
                if (dbContact.Owner != null)
                {
                    return dbContact.Owner.ID;
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
            var dbContact = db.CONTACTs.Find(id);
            if (dbContact != null)
            {
                if (dbContact.Collaborator != null)
                {
                    return dbContact.Collaborator.ID;
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

        public bool Update(int contactId, ContactCreateApiModel apiModel, int modifiedUser)
        {
            var dbContact = db.CONTACTs.Find(contactId);
            if (dbContact != null)
            {
                if(apiModel.owner != 0)
                {
                    dbContact.ContactOwner = apiModel.owner;

                }

                if (apiModel.collaborator != 0)
                {
                    dbContact.ContactCollaborator = apiModel.collaborator;
                }
                dbContact.Name = apiModel.name;
                dbContact.Email = apiModel.email;
                dbContact.Phone = apiModel.phone;
                dbContact.Mobile = apiModel.mobile;
                dbContact.DepartmentName = apiModel.departmentName;
                dbContact.Birthday = apiModel.birthday;

                if (apiModel.priority != 0)
                {
                    dbContact.PRIORITY_ID = apiModel.priority;

                }
                if (apiModel.account != 0)
                {
                    dbContact.ACCOUNT_ID = apiModel.account;
                }

                dbContact.NoEmail = apiModel.noEmail;
                dbContact.NoCall = apiModel.noCall;
                dbContact.Skype = apiModel.skype;
                dbContact.AssistantName = apiModel.assistantName;
                dbContact.AssistantPhone = apiModel.assistantPhone;

                dbContact.Country = apiModel.country;
                dbContact.City = apiModel.city;
                dbContact.ModifiedAt = DateTime.Now;
                dbContact.ModifiedBy = modifiedUser;
                db.SaveChanges();

                var modifyUser = db.USERs.Find(modifiedUser);
                var collaborator = db.USERs.Find(dbContact.ContactCollaborator);
                var createdUser = db.USERs.Find(dbContact.CreatedBy);


                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Contact updated";
                notifyModel.content = $"Contact {dbContact.Name} has been updated by {modifyUser.Username}.";
                notifyModel.module = "contacts";
                notifyModel.moduleObjectId = dbContact.ID;
                notifyModel.createdAt = DateTime.Now;
                notifyModel.module = "contacts";
                notifyModel.moduleObjectId = dbContact.ID;
                NotificationManager.SendNotification(notifyModel, new List<USER> { dbContact.Owner, collaborator, createdUser});
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var dbContact = db.CONTACTs.Find(id);
            if (dbContact != null)
            {
                var contactName = dbContact.Name;
                var owner = db.USERs.Find(dbContact.ContactOwner);
                var collaborator = db.USERs.Find(dbContact.ContactCollaborator);
                db.CONTACTs.Remove(dbContact);
                db.SaveChanges();
                var notifyModel = new NotificationApiModel();
                notifyModel.title = "Contact deleted";
                notifyModel.content = $"Contact {contactName} has been deleted.";
                notifyModel.createdAt = DateTime.Now;
                NotificationManager.SendNotification(notifyModel, new List<USER> { owner, collaborator });
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChangeAvatar(int id, string fileName)
        {
            var dbContact = db.CONTACTs.Find(id);
            if (dbContact != null)
            {
                dbContact.Avatar = fileName;
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
            var dbContact = db.CONTACTs.Find(id);
            var dbTag = _tagRepository.GetOneByName(tagName);
            if (dbContact != null)
            {
                if (dbTag != null)
                {
                    var tagItem = dbContact.TAG_ITEM.Where(c => c.TAG_ID == dbTag.ID).FirstOrDefault();
                    if (tagItem == null)
                    {
                        var newTagItem = new TAG_ITEM();
                        newTagItem.TAG_ID = dbTag.ID;
                        newTagItem.CONTACT_ID = dbContact.ID;
                        db.TAG_ITEM.Add(newTagItem);
                        db.SaveChanges();

                        var owner = db.USERs.Find(dbContact.ContactOwner);
                        var collaborator = db.USERs.Find(dbContact.ContactCollaborator);
                        var createdUser = db.USERs.Find(dbContact.CreatedBy);

                        var notifyModel = new NotificationApiModel();
                        notifyModel.title = "Tag added";
                        notifyModel.content = $"Tag {tagName} has been added to contact {dbContact.Name}.";
                        notifyModel.module = "contacts";
                        notifyModel.moduleObjectId = dbContact.ID;
                        notifyModel.createdAt = DateTime.Now;
                        NotificationManager.SendNotification(notifyModel, new List<USER> { owner, collaborator, createdUser });
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
                    tagItem.CONTACT_ID = dbContact.ID;
                    db.TAG_ITEM.Add(tagItem);
                    db.SaveChanges();
                    var owner = db.USERs.Find(dbContact.ContactOwner);
                    var collaborator = db.USERs.Find(dbContact.ContactCollaborator);
                    var createdUser = db.USERs.Find(dbContact.CreatedBy);
                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag added";
                    notifyModel.content = $"Tag {tagName} has been added to contact {dbContact.Name}.";
                    notifyModel.module = "contacts";
                    notifyModel.moduleObjectId = dbContact.ID;
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner, collaborator, createdUser });
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
            var dbContact = db.CONTACTs.Find(id);
            if (dbContact != null)
            {
                var tagItem = dbContact.TAG_ITEM.Where(c => c.TAG.ID == tagId).FirstOrDefault();
                if (tagItem != null)
                {
                    var tagName = tagItem.TAG.Name;
                    db.TAG_ITEM.Remove(tagItem);
                    db.SaveChanges();
                    var owner = db.USERs.Find(dbContact.ContactOwner);
                    var collaborator = db.USERs.Find(dbContact.ContactCollaborator);
                    var createdUser = db.USERs.Find(dbContact.CreatedBy);
                    var notifyModel = new NotificationApiModel();
                    notifyModel.title = "Tag removed";
                    notifyModel.content = $"Tag {tagName} has been removed from contact {dbContact.Name}.";
                    notifyModel.module = "contacts";
                    notifyModel.moduleObjectId = dbContact.ID;
                    notifyModel.createdAt = DateTime.Now;
                    NotificationManager.SendNotification(notifyModel, new List<USER> { owner, collaborator, createdUser });
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

        public (IEnumerable<TASK_TEMPLATE> tasks, Pager p) GetTasks(int contactId, int currentPage, int pageSize, string query)
        {
            var dbContact = db.CONTACTs.Find(contactId);
            if (dbContact != null)
            {
                var q = query.ToLower();
                var callList = db.CALLs.Where(c => c.CONTACT.ID == contactId).Select(c => c.TASK_TEMPLATE).ToList();
                var meetingParticipantList = db.MEETING_PARTICIPANT.Where(c => c.CONTACT.ID == contactId).Select(c => c.MEETING.TASK_TEMPLATE).ToList();
                var taskList = db.TASKs.Where(c => c.CONTACT.ID == contactId).Select(c => c.TASK_TEMPLATE).ToList();

                var allTasks = new List<TASK_TEMPLATE>();
                allTasks.AddRange(callList);
                allTasks.AddRange(meetingParticipantList);
                allTasks.AddRange(taskList);

                if (pageSize == 0)
                {
                    pageSize = 10;
                }

                if (String.IsNullOrEmpty(q))
                {
                    Pager pager = new Pager(allTasks.Count(), currentPage, pageSize, 9999);
                    return (allTasks.OrderByDescending(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize), pager);
                }
                var filtered = allTasks.Where(c => c.Title.Contains(q)).OrderByDescending(c => c.ID);
                if (filtered.Count() > 0)
                {
                    Pager p = new Pager(filtered.Count(), currentPage, pageSize, 9999);

                    return (filtered.Skip((currentPage - 1) * pageSize).Take(pageSize), p);
                }
                else
                {
                    return (filtered, null);
                }
            }
            else
            {
                return (null, null);
            }
        }
    }
}