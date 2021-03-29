using Backend.Models.ApiModel;
using Backend.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Validators
{
    public class CallValidator : AbstractValidator<CallCreateApiModel>
    {
        public CallValidator()
        {
            RuleFor(c => c.title).NotEmpty().WithMessage(ErrorMessages.TITLE_EMPTY);
        }
    }
}