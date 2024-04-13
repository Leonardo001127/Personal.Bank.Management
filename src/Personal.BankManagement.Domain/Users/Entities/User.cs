namespace Personal.BankManagement.Domain;

public class User
{

    public Guid Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public Guid PersonId { get; set; }

    public virtual Person Person { get; set; }

}
