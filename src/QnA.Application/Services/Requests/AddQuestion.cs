namespace QnA.Application.Services.Requests;

public class AddQuestion : IRequest<bool>
{
    public AddQuestion(string content, int userId)
    {
        Content = content;
        UserId = userId;
    }

    public string Content { get; }

    public int UserId { get;}
}
