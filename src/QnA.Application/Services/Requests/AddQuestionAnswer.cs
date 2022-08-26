namespace QnA.Application.Services.Requests;

public class AddQuestionAnswer : IRequest<bool>
{
    public AddQuestionAnswer(int questionId,int userId,string answer)
    {
        QuestionId = questionId;
        UserId = userId;
        Answer = answer;
    }

    public int QuestionId { get; }

    public int UserId { get;}

    public string Answer { get; }
}
