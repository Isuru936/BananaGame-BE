using BananaGame.Application.Features.Auth.Command;
using FluentValidation;

namespace BananaGame.Application.Features.Auth.Validator
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("User Cannot be Empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("User Email Cannot be Empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("User Password Cannot be Empty");
        }
    }
}
