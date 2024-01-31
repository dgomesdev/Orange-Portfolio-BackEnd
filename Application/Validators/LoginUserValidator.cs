using FluentValidation;
using Orange_Portfolio_BackEnd.Application.ViewModel;

namespace Orange_Portfolio_BackEnd.Application.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginViewModel>
    {
        public LoginUserValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .MaximumLength(60)
                .WithMessage("Email supports a maximum of 60 characters")
                .EmailAddress()
                .WithMessage("Invalid email format");

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .Length(6, 90)
                .WithMessage("Password must be between 6 and 90 characters ");
        }
    }
}
