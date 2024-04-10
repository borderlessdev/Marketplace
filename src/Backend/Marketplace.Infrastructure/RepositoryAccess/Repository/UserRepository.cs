using Marketplace.Domain.Entity;
using Marketplace.Domain.Repository.User;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.RepositoryAccess.Repository;

public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
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

    public async Task<bool> ExistsUserByEmail(string email)
    {
        return await _context.Users.AnyAsync(user => user.Email.Equals(email));
    }

    public async Task<User> Login(string email, string senha)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Password.Equals(senha));
    }
}
