using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Backend.Repository;
using Backend.Models;
using Backend.Validators;
using FluentValidation.Results;
using Backend.Extensions;
using Backend.Domain;
using Backend.Resources;
using Backend.Models.ApiModel;

namespace Backend.Services
{
    public class UserService
    {
        UserRepository _userRepository = new UserRepository();
        UserValidator _validator = new UserValidator();
        HashManager _hashManager = new HashManager();
        DatabaseContext db = new DatabaseContext();
        public USER GetOne(int id)
        {
            try
            {
                return db.USERs.Find(id);
            }
            catch
            {
                throw;
            }
        }

        public USER GetOneByEmail(string email)
        {
            try
            {
                return db.USERs.Where(c => c.Email == email).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public (List<USER>, Pager) GetAll(int currentPage = 0, int pageSize = 0)
        {
            try
            {
                if (currentPage == 0 && pageSize == 0)
                {
                    Pager p = new Pager(db.USERs.Count(), 1, db.USERs.Count());
                    return (db.USERs.OrderBy(c => c.ID).ToList(), p);
                }
                else
                {
                    Pager p = new Pager(db.USERs.Count(), currentPage, pageSize);
                    return (db.USERs.OrderBy(c => c.ID).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(), p);
                }
            }
            catch
            {
                throw;
            }
            
        }

        public Pager GetPagerInfo(int currentPage = 0, int pageSize = 10)
        {
            return new Pager(db.USERs.Count(), currentPage, pageSize);
        }

        public ValidationResult Validate(User u)
        {
            return _validator.Validate(u);
        }

        public (bool, string, User) ValidatePassword(string email, string password)
        {
            try
            {
                var dbuser = _userRepository.GetByEmail(email);
                var user = AutoMapper.Mapper.Map<User>(dbuser);
                if(dbuser != null)
                {
                    if(_hashManager.VerifyHash(password, dbuser.Hash))
                    {
                        return (true, "", user);
                    }
                }
                return (false, ErrorMessages.WRONG_PASSWORD, null);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">Api model object</param>
        /// <param name="userCreated">ID of current user</param>
        /// <returns></returns>
        public USER Create(User user, int userCreated = 0)
        {
            var newDbUser = new USER();
            newDbUser.FirstName = user.FirstName;
            newDbUser.LastName = user.LastName;
            newDbUser.GROUP_ID = user.GROUP_ID;
            newDbUser.Phone = user.Phone;
            newDbUser.Skype = user.Skype;
            newDbUser.Email = user.Email;
            newDbUser.Username = user.Username;
            if(userCreated != 0)
            {
                newDbUser.CreatedBy = userCreated;
            }
            newDbUser.CreatedAt = DateTime.Now;
            //hash the user password
            newDbUser.Hash = _hashManager.Hash(user.Password);
            db.USERs.Add(newDbUser);
            db.SaveChanges();
            return newDbUser;
        }

        public List<UserSelectionApiModel> GetUsersForSelection()
        {
            var users = _userRepository.GetAll().Select(c => new UserSelectionApiModel() { id = c.ID, username = c.Username, selected = false }).ToList();
            return users;
        }

        public UserListApiModel GetUserList(string query = "", int pageSize = 0, int currentPage = 1)
        {
            var dbUsers = _userRepository.GetAllUsers(query, pageSize, currentPage);
            var apiModel = new UserListApiModel();
            string targetFolder = HttpContext.Current.Server.MapPath("~/Uploads");

            apiModel.users = dbUsers.users.Select(c => new UserListApiModel.UserInfo() { id = c.ID, username = c.Username, firstName = c.FirstName, lastName = c.LastName, email = c.Email, phone = c.Phone, skype = c.Skype, avatar = c.Avatar != null ? $"{StaticStrings.ServerHost}avatar?fileName={c.Avatar}" : $"{StaticStrings.ServerHost}avatar?fileName=default.png" }).ToList();
            apiModel.pageInfo = dbUsers.p;
            return apiModel;
        }
    }
}