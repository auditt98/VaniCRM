using Backend.Models.ApiModel;
using Backend.Resources;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Validators
{
    public class CampaignValidator : AbstractValidator<CampaignCreateApiModel>
    {
        public CampaignValidator()
        {
            RuleFor(c => c.campaignName).NotEmpty().WithMessage(ErrorMessages.NAME_EMPTY);
            RuleFor(c => c.startDate).LessThan(c => c.endDate).WithMessage(ErrorMessages.DATE_START_LESS_THAN_END);
        }
    }
}