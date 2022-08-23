namespace QnA.Application.Services.Requests;

public class Register : IRequest<IdentityResult>
{
    public string Email { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }
}
