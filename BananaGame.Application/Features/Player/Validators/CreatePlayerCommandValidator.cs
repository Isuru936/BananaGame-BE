using BananaGame.Application.Features.Player.Commands;
using FluentValidation;

namespace BananaGame.Application.Features.Player.Validators
{
    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        public CreatePlayerCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 20 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 20 characters.");

        }
    }
}
