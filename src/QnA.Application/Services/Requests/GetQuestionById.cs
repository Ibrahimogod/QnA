namespace QnA.Application.Services.Requests;

public class GetQuestionById : IRequest<QuestionModel>
{
    public int QuestionId { get; set; }
}
