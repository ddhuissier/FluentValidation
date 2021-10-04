using FluentValidation;
using FluentValidationLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidationUI.Validators
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 30).WithMessage("{PropertyName} is invalid")
                .Must(BeAValideName).WithMessage("Bad characters");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("{PropertyName} is empty")
                .Length(2, 30).WithMessage("{PropertyName} is invalid")
                .Must(BeAValideName).WithMessage("Bad characters");

            RuleFor(x => x.DateOfBirth)
                .Must(BeAValideDateTime);

            // more examples https://fluentvalidation.net/
        }

        protected bool BeAValideName(string name)
        {
            name = name.Replace("-", "");
            name = name.Replace(" ", "");
            return name.All(Char.IsLetter);
        }

        protected bool BeAValideDateTime(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear && dobYear > (currentYear - 120))
            {
                return true;
            }
            return false;
        }
    }
}
