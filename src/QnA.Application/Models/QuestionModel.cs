namespace QnA.Application.Models;

public class QuestionModel
{
    public QuestionModel()
    {
        Answers = Enumerable.Empty<AnswerModel>();
    }

    public int Id { get; set; }

    public string Content { get; set; }

    public string Username { get; set; }

    public int Rank { get; set; }

    public IEnumerable<AnswerModel> Answers {get;set;}
}
