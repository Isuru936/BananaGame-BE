using BananaGame.Application.Shared;
using FluentValidation.Results;

namespace BananaGame.Application.Exceptions
{
    public class ValdationExceptions : Exception
    {
        public IReadOnlyCollection<ValidationFailure> Errors { get; }

        public ValdationExceptions(IReadOnlyCollection<ValidationFailure> errors)
            : base(CreateErrorMessage(errors))
        {
            Errors = errors;
        }

        private static string CreateErrorMessage(IEnumerable<ValidationFailure> failures) =>
            string.Join("; ", failures.Select(f => $"{f.PropertyName}: {f.ErrorMessage}"));
    }

    public sealed record ValidationError : Error
    {
        public ValidationError(string propertyName, Error[] errors)
            : base("General.ValidationFailed", string.Join("; ", errors.Select(e => e.description)))
        {
            Errors = errors;
        }

        public Error[] Errors { get; }

        public static ValidationError FromResult(IEnumerable<Result> results)
        {
            var errors = results.Where(r => r.IsFailure).Select(r => r.Error).ToArray();
            return new ValidationError("PropertyName", errors);
        }
    }
}
