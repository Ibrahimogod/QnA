namespace QnA.Core.Domains.Questions;

public class Question : BaseEntity
{
    public Question()
    {
        Answers = new HashSet<Answer>();
    }

    public string Content { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Answer> Answers { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}
