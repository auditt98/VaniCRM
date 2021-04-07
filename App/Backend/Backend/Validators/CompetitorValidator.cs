using Backend.Models.ApiModel;
using Backend.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Validators
{
    public class CompetitorValidator : AbstractValidator<CompetitorCreateApiModel>
    {
        public CompetitorValidator()
        {
            RuleFor(c => c.name).NotEmpty().WithMessage(ErrorMessages.NAME_EMPTY);
        }
    }
}