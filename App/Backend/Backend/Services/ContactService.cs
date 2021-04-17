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
    public class ContactService
    {
        ContactRepository _contactRepository = new ContactRepository();
        ContactValidator _contactValidator = new ContactValidator();
        PriorityRepository _priorityRepository = new PriorityRepository();
        public List<CONTACT> GetUserContacts(int userID, string q = "", int currentPage = 1, int pageSize = 0)
        {
            return _contactRepository.GetUserContacts(userID, q, currentPage, pageSize).ToList();

        }

        public ContactListApiModel GetContactList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbContacts = _contactRepository.GetAllContacts(query, pageSize, currentPage);
            var apiModel = new ContactListApiModel();

            apiModel.contacts = dbContacts.contacts.Select(c => new ContactListApiModel.ContactInfo() { id = c.ID, contactName = c.Name, accountName = c.ACCOUNT != null ? c.ACCOUNT.Name : "", email = c.Email, phone = c.Phone, owner = c.Owner.FirstName + " " + c.Owner.LastName }).ToList();
            apiModel.pageInfo = dbContacts.p;
            return apiModel;
        }

        public bool Create(ContactCreateApiModel apiModel, int createdUser)
        {
            var validator = _contactValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _contactRepository.Create(apiModel, createdUser);
            }
            return false;
        }

        public int FindOwnerId(int contactId)
        {
            return _contactRepository.GetOwner(contactId);
        }

        public int FindCollaboratorId(int contactId)
        {
            return _contactRepository.GetCollaborator(contactId);
        }

        public bool Update(int id, ContactCreateApiModel apiModel, int modifiedUser)
        {
            var validator = _contactValidator.Validate(apiModel);
            if (validator.IsValid)
            {
                return _contactRepository.Update(id, apiModel, modifiedUser);
            }
            return false;
        }

        public bool Delete(int id)
        {
            return _contactRepository.Delete(id);
        }

        public bool ChangeAvatar(int id, HttpPostedFile uploadedFile)
        {
            FileManager.File file = new FileManager.File(uploadedFile);
            var isImage = file.FilterExtension(new List<string>() { ".jpeg", ".jpg", ".png", ".tif", ".tiff" });
            if (isImage)
            {
                file.Rename();
                var isChanged = _contactRepository.ChangeAvatar(id, file.FullName);
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
            return _contactRepository.AddTag(id, tagName);
        }

        public bool RemoveTag(int id, int tagId)
        {
            return _contactRepository.RemoveTag(id, tagId);
        }

        public ContactDetailApiModel GetOne(int id)
        {
            string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");
            var dbContact = _contactRepository.GetOne(id);
            if (dbContact != null)
            {
                var apiModel = new ContactDetailApiModel();

                apiModel.id = dbContact.ID;
                //avatar
                if (dbContact.Avatar != null)
                {
                    //var mime = MimeMapping.MimeUtility.GetMimeMapping(dbLead.Avatar);
                    //var img = Convert.ToBase64String(System.IO.File.ReadAllBytes(Path.Combine(targetFolder, dbLead.Avatar)));
                    apiModel.avatar = $"{StaticStrings.ServerHost}avatar?fileName={dbContact.Avatar}";
                }

                apiModel.name = dbContact.Name;
                apiModel.email = dbContact.Email;
                apiModel.phone = dbContact.Phone;
                apiModel.mobile = dbContact.Mobile;
                apiModel.departmentName = dbContact.DepartmentName;
                apiModel.birthday = dbContact.Birthday.GetValueOrDefault();
                apiModel.noCall = dbContact.NoCall.GetValueOrDefault();
                apiModel.noEmail = dbContact.NoEmail.GetValueOrDefault();
                apiModel.skype = dbContact.Skype;
                apiModel.assistantName = dbContact.AssistantName;
                apiModel.assistantPhone = dbContact.AssistantPhone;   
                apiModel.country = dbContact.Country;
                apiModel.city = dbContact.City;
                apiModel.addressDetail = dbContact.AddressDetail;

                apiModel.CreatedAt = dbContact.CreatedAt.GetValueOrDefault();
                apiModel.CreatedBy = new UserLinkApiModel() { id = dbContact.CreatedUser.ID, username = dbContact.CreatedUser.Username, email = dbContact.CreatedUser.Email };
                apiModel.ModifiedAt = dbContact.ModifiedAt.GetValueOrDefault();
                apiModel.ModifiedBy = new UserLinkApiModel() { id = dbContact.ModifiedUser?.ID, username = dbContact.ModifiedUser?.Username, email = dbContact.ModifiedUser?.Email };

                apiModel.owner = new UserLinkApiModel() { id = dbContact.Owner?.ID, username = dbContact.Owner?.Username, email = dbContact.Owner?.Email };
                apiModel.collaborator = new UserLinkApiModel() { id = dbContact.Collaborator?.ID, username = dbContact.Collaborator?.Username, email = dbContact.Collaborator?.Email };

                if (dbContact.ACCOUNT != null)
                {
                    apiModel.account = new AccountLinkApiModel() { id = dbContact.ACCOUNT.ID, email = dbContact.ACCOUNT.Email, name = dbContact.ACCOUNT.Name };
                }
                //notes
                apiModel.notes = dbContact.NOTEs.Select(c => new NoteApiModel() { id = c.ID, avatar = $"{StaticStrings.ServerHost}avatar?fileName={c.USER?.Avatar}", body = c.NoteBody, createdAt = c.CreatedAt.GetValueOrDefault(), createdBy = new UserLinkApiModel() { id = c.USER.ID, username = c.USER.Username, email = c.USER.Email }, files = c.FILEs.Select(f => new FileApiModel() { id = f.ID, fileName = f.FileName, size = f.FileSize.Value.ToString() + " KB", url = StaticStrings.ServerHost + "files/" + f.ID }).ToList() }).ToList();

                //tags
                apiModel.tags = dbContact.TAG_ITEM.Select(c => new TagApiModel() { id = c.TAG.ID, name = c.TAG.Name }).ToList();
                return apiModel;
            }
            else
            {
                return null;
            }
        }

        public TaskListApiModel GetTasks(int contactId, int currentPage, int pageSize, string query)
        {
            var dbTasks = _contactRepository.GetTasks(contactId, currentPage, pageSize, query);
            var apiModel = new TaskListApiModel();
            apiModel.tasks = new List<TaskListApiModel.TaskInfo>();
            foreach(var t in dbTasks.tasks)
            {
                var taskInfo = new TaskListApiModel.TaskInfo();
                if(t.MEETINGs.Count > 0)
                {
                    var meeting = t.MEETINGs.FirstOrDefault();
                    if(meeting != null)
                    {
                        taskInfo.id = meeting.ID;
                        taskInfo.startDate = meeting.FromDate.GetValueOrDefault();
                        taskInfo.owner = new UserLinkApiModel() { id = meeting.HostUser.ID, email = meeting.HostUser.Email, username = meeting.HostUser.Username };
                        taskInfo.type = "meetings";
                        if(t.PRIORITY != null)
                        {
                            taskInfo.priority = t.PRIORITY.Name;
                        }
                        taskInfo.endDate = meeting.ToDate.GetValueOrDefault();
                        if(t.TASK_STATUS != null)
                        {
                            taskInfo.status = t.TASK_STATUS.Name;

                        }
                        apiModel.tasks.Add(taskInfo);
                    }
                    else
                    {
                        return null;
                    }
                }
                else if(t.CALLs.Count > 0)
                {
                    var call = t.CALLs.FirstOrDefault();
                    if (call != null)
                    {
                        taskInfo.id = call.ID;
                        taskInfo.startDate = t.CreatedAt.GetValueOrDefault();
                        taskInfo.owner = new UserLinkApiModel() { id = call.Owner.ID, email = call.Owner.Email, username = call.Owner.Username };
                        taskInfo.type = "calls";
                        if (t.PRIORITY != null)
                        {
                            taskInfo.priority = t.PRIORITY.Name;
                        }
                        taskInfo.endDate = t.DueDate.GetValueOrDefault();
                        if (t.TASK_STATUS != null)
                        {
                            taskInfo.status = t.TASK_STATUS.Name;

                        }
                        apiModel.tasks.Add(taskInfo);

                    }
                    else
                    {
                        return null;
                    }
                }
                else if(t.TASKs.Count > 0)
                {
                    var task = t.TASKs.FirstOrDefault();
                    if (task != null)
                    {
                        taskInfo.id = task.ID;
                        taskInfo.startDate = t.CreatedAt.GetValueOrDefault();
                        taskInfo.owner = new UserLinkApiModel() { id = task.USER.ID, email = task.USER.Email, username = task.USER.Username };
                        taskInfo.type = "tasks";
                        if (t.PRIORITY != null)
                        {
                            taskInfo.priority = t.PRIORITY.Name;
                        }
                        taskInfo.endDate = t.DueDate.GetValueOrDefault();
                        if (t.TASK_STATUS != null)
                        {
                            taskInfo.status = t.TASK_STATUS.Name;

                        }
                        apiModel.tasks.Add(taskInfo);

                    }
                    else
                    {
                        return null;
                    }
                }
                //taskInfo.id = t.ID
            }
            //apiModel.tasks = dbTasks.tasks.Select(c => new TaskListApiModel.TaskInfo() { id = c.ID, type = c.}).ToList();
            apiModel.pageInfo = dbTasks.p;
            return apiModel;
        }

        public ContactBlankApiModel PrepareNewContact()
        {
            var apiModel = new ContactBlankApiModel();
            apiModel.priority = _priorityRepository.GetAllPriorities().Select(c => new PrioritySelectionApiModel() { id = c.ID, name = c.Name, selected = false }).ToList();
            return apiModel;
        }
    }
}