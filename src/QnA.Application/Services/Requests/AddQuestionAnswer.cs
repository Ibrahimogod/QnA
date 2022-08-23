namespace QnA.Application.Services.Requests;

public class AddQuestionAnswer : IRequest<bool>
{
    public int QuestionId { get; set; }

    public int UserId { get; set; }

    public string Answer { get; set; }
}
