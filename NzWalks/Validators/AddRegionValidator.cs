using System;
using FluentValidation;
using NzWalks.Model.DTO;

namespace NzWalks.Validators
{
    public class NewRegionDTOValidator : AbstractValidator<NewRegionDTO>
    {
        public NewRegionDTOValidator()
        {
            RuleFor(x=>x.Code).NotEmpty();
            RuleFor(x=>x.Name).NotEmpty();
            RuleFor(x=>x.Area).GreaterThan(0);
            RuleFor(x=>x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
