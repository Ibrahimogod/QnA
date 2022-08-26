namespace QnA.Application.Services.Requests;

public class Register : IRequest<IdentityResult>
{
    public Register(string email, string username, string password)
    {
        Email = email;
        Username = username;
        Password = password;
    }

    public string Email { get; }

    public string Username { get; }

    public string Password { get; }
}
