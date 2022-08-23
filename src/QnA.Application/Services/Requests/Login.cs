namespace QnA.Application.Services.Requests;

public class Login : IRequest<AccessTokenResponse>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
