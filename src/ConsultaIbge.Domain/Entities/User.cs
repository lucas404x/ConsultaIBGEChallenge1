namespace ConsultaIbge.Domain.Entities;

public class User
{
    public User(string id, string email, string password)
    {
        Id = id;
        Email = email;
        Password = password;
    }

    public string Id { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}
