namespace BananaGame.Application.Abstractions.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellation);
    }
}
