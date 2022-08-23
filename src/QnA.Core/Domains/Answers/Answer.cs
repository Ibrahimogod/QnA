namespace QnA.Core.Domains.Answers;

public class Answer : BaseEntity
{
    public string Content { get; set; }

    public int QuestionId { get; set; }

    public int UserId { get; set; }

    [ForeignKey("QuestionId")]
    public virtual Question Question { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    public virtual ICollection<Vote> Votes { get; set; }
}
