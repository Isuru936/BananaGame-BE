using BananaGame.Application.Features.Auth.Command;
using FluentValidation;

namespace BananaGame.Application.Features.Auth.Validator
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("User Cannot be Empty");
            RuleFor(x => x.Username).NotEmpty().WithMessage("User Cannot be Empty");
        }
    }
}
