using DirectoryService.Contracts;
using FluentValidation;

namespace DirectoryService.Application.Locations
{
    public class CreateLocationValidator : AbstractValidator<CreateLocationDto>
    {
        public CreateLocationValidator()
        {
            RuleFor(l => l.Address).NotNull().WithMessage("Address is not a null");

            RuleFor(l => l.Address).NotEmpty().WithMessage("Address is not a empty");

            RuleFor(l => l.Name).NotNull().WithMessage("Name is not a null");

            RuleFor(l => l.Name).NotEmpty().WithMessage("Name is not a empty");

            RuleFor(l => l.Timezone).NotNull().WithMessage("Time zone is not a null");

            RuleFor(l => l.Timezone).NotEmpty().WithMessage("Time zone is not a empty");            
        }
    }
}
