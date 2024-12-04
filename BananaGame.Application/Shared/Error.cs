namespace BananaGame.Application.Shared
{
    public record Error(string code, string? description = null)
    {
        public static readonly Error None = new(string.Empty);

        public static Error Failure(string code, string description) =>
            new(code, description);
    }
}
