using BananaGame.Application.Shared;
using FluentValidation;
using MediatR;

namespace BananaGame.Application.Behaviours
{
    public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>>
    where TRequest : IRequest<Result<TResponse>>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            var errors = validationFailures
                .Where(v => !v.IsValid)
                .SelectMany(v => v.Errors)
                .ToArray();

            if (errors.Any())
            {
                var errorMessage = string.Join("; ", errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
                var error = new Error("ValidationError", errorMessage);
                return Result.Failure<TResponse>(default, error);
            }

            return await next();
        }
    }
}
