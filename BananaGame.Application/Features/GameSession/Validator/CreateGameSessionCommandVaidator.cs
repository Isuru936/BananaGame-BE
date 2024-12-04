using BananaGame.Application.Features.GameSession.Commands;
using FluentValidation;

namespace BananaGame.Application.Features.GameSession.Validator
{
    public class CreateGameSessionCommandVaidator : AbstractValidator<StartGameSessionCommand>
    {
        public CreateGameSessionCommandVaidator()
        {
            RuleFor(x => x.PlayerId)
                .NotEmpty().WithMessage("PlayerId is required.");
        }
    }
}
