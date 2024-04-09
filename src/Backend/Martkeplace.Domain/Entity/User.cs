namespace Martkeplace.Domain.Entity;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Cpf { get; set; }
    public string Password { get; set; }

}
