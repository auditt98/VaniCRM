using Backend.Domain;
using Backend.Models.ApiModel;
using Backend.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Validators
{
    public class LeadValidator: AbstractValidator<LeadCreateApiModel>
    {
        public LeadValidator()
        {
            RuleFor(c => c.name).NotEmpty().WithMessage(ErrorMessages.LEAD_NAME_INVALID);
            RuleFor(c => c.email).NotEmpty().WithMessage(ErrorMessages.EMAIL_EMPTY);
            RuleFor(c => c.phone).MaximumLength(15).WithMessage(ErrorMessages.PHONE_LENGTH_TOO_LONG + " Maximum is 15 characters");
            RuleFor(c => c.website).MaximumLength(100).WithMessage(ErrorMessages.WEBSITE_TOO_LONG + " Maximum is 100 characters");
            RuleFor(c => c.skype).MaximumLength(32).WithMessage(ErrorMessages.SKYPE_TOO_LONG + " Maximum is 32 characters");
            RuleFor(c => c.companyName).MaximumLength(150).WithMessage(ErrorMessages.COMPANY_NAME_TOO_LONG + " Maximum is 150 characters");
            RuleFor(c => c.addressDetail).MaximumLength(200).WithMessage(ErrorMessages.ADDRESS_LENGTH_TOO_LONG + " Maximum is 200 characters");
            RuleFor(c => c.country).MaximumLength(100).WithMessage(ErrorMessages.COUNTRY_NAME_TOO_LONG + " Maximum is 100 characters");
            RuleFor(c => c.city).MaximumLength(100).WithMessage(ErrorMessages.CITY_NAME_TOO_LONG + " Maximum is 100 characters");

        }
    }
}