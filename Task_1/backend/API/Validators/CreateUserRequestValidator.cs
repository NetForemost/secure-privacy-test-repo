using Backend.API.Requests;
using FluentValidation;

namespace Backend.API.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Email address has invalid format.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+\d{1,3}\d{1,14}(?:x.+)?$").WithMessage("Phone number has invalid format.")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber)).WithMessage("Phone number is required.");
    }
}
