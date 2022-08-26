namespace QnA.Application.Services.Requests;

public class DeleteAsnswer : IRequest<bool>
{
    public DeleteAsnswer(int asnswerId, int questionId, int userId)
    {
        AsnswerId = asnswerId;
        QuestionId = questionId;
        UserId = userId;
    }

    public int AsnswerId { get; }

    public int QuestionId { get;  }

    public int UserId { get; }
}
