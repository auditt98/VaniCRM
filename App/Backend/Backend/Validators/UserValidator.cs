using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Backend.Models;
using Backend.Domain;
using Backend.Resources;

namespace Backend.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email).NotEmpty().WithMessage(ValidationErrorMessages.EMAIL_EMPTY);
            RuleFor(user => user.Email).EmailAddress().WithMessage(ValidationErrorMessages.EMAIL_WRONG_FORMAT);
            RuleFor(user => user.Password).NotNull().MinimumLength(4).WithMessage(ValidationErrorMessages.PASSWORD_LENGTH_NOT_VALID);
            RuleFor(user => user.Email).Must(IsEmailUnique).WithMessage(ValidationErrorMessages.EMAIL_OCCUPIED);
            RuleFor(user => user.FirstName).NotEmpty().WithMessage(ValidationErrorMessages.FIRSTNAME_EMPTY);
            RuleFor(user => user.LastName).NotEmpty().WithMessage(ValidationErrorMessages.LASTNAME_EMPTY);
            RuleFor(user => user.Username).NotEmpty().WithMessage(ValidationErrorMessages.USERNAME_EMPTY);

        }

        public bool IsEmailUnique(User user, string email)
        {
            using(var db = new DatabaseContext())
            {
                var dbUser = db.USERs.Where(c => c.Email.ToLower() == email.ToLower()).FirstOrDefault();
                if(dbUser == null)
                {
                    return true;
                }
                return false;
            }
        }
    }

}