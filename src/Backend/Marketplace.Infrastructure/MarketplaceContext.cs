using Martkeplace.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure;

public class MarketplaceContext : DbContext
{
    public MarketplaceContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof (MarketplaceContext).Assembly);
    }
}
