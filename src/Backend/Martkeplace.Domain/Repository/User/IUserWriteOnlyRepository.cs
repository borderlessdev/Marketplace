
public interface IUserWriteOnlyRepository
{
    Task Create(Marketplace.Domain.Entity.User user);
}
