using Backend.Models.ApiModel;
using Backend.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Validators
{
    public class ContactValidator : AbstractValidator<ContactCreateApiModel>
    {
        public ContactValidator()
        {
            RuleFor(c => c.name).NotEmpty().WithMessage(ErrorMessages.NAME_EMPTY);
        }
    }
}