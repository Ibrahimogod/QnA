namespace QnA.Application.Services.Requests;

public class DeleteAsnswer : IRequest<bool>
{
    public int AsnswerId { get; set; }

    public int QuestionId { get; set; }

    public int UserId { get; set; }
}
