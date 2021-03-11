using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Backend.Models;
namespace Backend.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email must not be empty");
            RuleFor(user => user.Email).EmailAddress().WithMessage("Email format is incorrect");
        }
    }

}