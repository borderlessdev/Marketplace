namespace Marketplace.Domain.Repository.User;

public interface IUserWriteOnlyRepository
{
    Task Create(Entity.User user);
}
