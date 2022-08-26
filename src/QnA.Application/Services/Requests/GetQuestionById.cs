namespace QnA.Application.Services.Requests;

public class GetQuestionById : IRequest<QuestionModel>
{
    public GetQuestionById(int questionId)
    {
        QuestionId = questionId;
    }

    public int QuestionId { get; }
}
