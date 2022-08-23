namespace QnA.Application.Services.Requests;

public class AddVote : IRequest<bool>
{
    public int QuestionId { get; set; }

    public int AnswerId { get; set; }

    public int UserId { get; set; }

    public bool? IsUpVote { get; set; }
}
