namespace CollectR.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    Task SaveChangesAsync(CancellationToken ct);
}
