namespace Marketplace.Domain.Repository.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistsUserByEmail(string email);
    Task<Entity.User> Login(string email, string senha);
}
