namespace QnA.Application.Services.Requests;

public class AddVote : IRequest<bool>
{
    public AddVote(int questionId, int answerId, int userId, bool? isUpVote)
    {
        QuestionId = questionId;
        AnswerId = answerId;
        UserId = userId;
        IsUpVote = isUpVote;
    }

    public int QuestionId { get; }

    public int AnswerId { get; }

    public int UserId { get; }

    public bool? IsUpVote { get;}
}
