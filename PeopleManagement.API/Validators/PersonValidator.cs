using FluentValidation;
using PeopleManagement.API.Model;

namespace PeopleManagement.API.Validators
{
    public class PersonValidator : AbstractValidator<PersonModel>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).NotEmpty().Length(2,50).WithMessage("Please provide a valid first name.");
            RuleFor(p => p.Surname).NotEmpty().Length(2, 50).WithMessage("Please provide a valid surname.");
        }
    }
}
