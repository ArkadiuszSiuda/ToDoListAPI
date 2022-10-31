using FluentValidation;
using ToDoListAPI.Models;

namespace ToDoListAPI.Validation;

public class LoginValidator : AbstractValidator<Login>
{
    public LoginValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty().WithMessage("Email address cannot be empty.")
            .EmailAddress().WithMessage("Provide correct email address.");

        RuleFor(l => l.Password)
            .NotEmpty().WithMessage("Password cannot be empty.");
    }
}