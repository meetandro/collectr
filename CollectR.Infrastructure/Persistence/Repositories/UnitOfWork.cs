using CollectR.Application.Contracts.Persistence;

namespace CollectR.Infrastructure.Persistence.Repositories;

internal sealed class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;

    public Task SaveChangesAsync(CancellationToken cancellationToken) // createdat updatedat and isdeleted configuration
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
