namespace QnA.Application.Services.Requests;

public class Login : IRequest<AccessTokenResponse>
{
    public Login(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; }

    public string Password { get; }
}
