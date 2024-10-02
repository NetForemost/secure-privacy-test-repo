using Backend.API.Requests;
using FluentValidation;

namespace Backend.API.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.PhoneNumber).Matches(@"^\+\d{1,3}\d{1,14}(?:x.+)?$")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
    }
}
