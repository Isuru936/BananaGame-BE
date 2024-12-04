using BananaGame.Application.Features.Player.Commands;
using FluentValidation;

namespace BananaGame.Application.Features.Player.Validators
{
    public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
    {
        public UpdatePlayerCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username cannot be Empty");
        }
    }
}
