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

namespace Backend.Services
{
    public class UserService
    {
        UserRepository _userRepository = new UserRepository();
        UserValidator _validator = new UserValidator();
        Hashing hashValidator = new Hashing();
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
                    if(hashValidator.VerifyHash(password, dbuser.Hash))
                    {
                        return (true, "", user);
                    }
                }
                return (false, "Email/Password is incorrect", null);
            }
            catch
            {
                throw;
            }
        }
    }
}