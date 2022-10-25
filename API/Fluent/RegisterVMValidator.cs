using FluentValidation;
using Met.DTOs;

namespace API.Fluent
{
    public class RegisterVMValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterVMValidator()
        {
            RuleFor(x => x.EmailAddress)
                .NotNull().WithMessage("Email Shouldn't be null");

            RuleFor(x => x.EmailAddress)
               .NotEmpty()
               .WithMessage("EmailAddress Shouldn't be null");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName Shouldn't be null");

        }
    }
}
