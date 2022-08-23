namespace QnA.Application.Services.Requests;

public class AddQuestion : IRequest<bool>
{
    public string Content { get; set; }

    public int UserId { get; set; }
}
