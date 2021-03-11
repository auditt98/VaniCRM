using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Backend.Repository;
using Backend.Models;
using Backend.Validators;
using FluentValidation.Results;
using Backend.Extensions;
namespace Backend.Services
{
    public class UserService
    {
        UserRepository _userRepository = new UserRepository();
        UserValidator _validator = new UserValidator();
        Hashing hashValidator = new Hashing();
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