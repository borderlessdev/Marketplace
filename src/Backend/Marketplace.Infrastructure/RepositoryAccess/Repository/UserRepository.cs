//using Martkeplace.Domain.Entity.UserEntity;

using Marketplace.Domain.Repository.User;
using Martkeplace.Domain.Entity;

namespace Marketplace.Infrastructure.RepositoryAccess.Repository;

public class UserRepository : IUserWriteOnlyRepository
{
    private readonly MarketplaceContext _context;

    public UserRepository(MarketplaceContext context)
    {
        _context = context;
    }

    public async Task Create(User user)
    {
        await _context.Users.AddAsync(user);
    }
}
