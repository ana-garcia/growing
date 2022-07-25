using FluentValidation;
using PeopleManagement.API.Model;

namespace PeopleManagement.API.Validators
{
    public class RegistrationValidator : AbstractValidator<RegistrationForCreationModel>
    {
        public RegistrationValidator()
        {
            RuleFor(p => p.Motive).NotEmpty().MaximumLength(200);
            RuleFor(p => p.ExpectedDuration).NotEmpty().GreaterThan(0);
        }
    }
}
