using Marketplace.Domain.Repository;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Transactions;

namespace Marketplace.Infrastructure.RepositoryAccess;

public class WorkUnity : IWorkUnity
{
    private readonly MarketplaceContext _context;
    private bool _disposed;

    public WorkUnity(MarketplaceContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    public void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }
}
