namespace QnA.Core.Domains.Votes;

public class Vote : BaseEntity
{
    public bool IsUpVote { get; set; }

    public int AnswerId { get; set; }

    public int UserId { get; set; }

    [ForeignKey("AnswerId")]
    public virtual Answer Answer {get;set;}

    [ForeignKey("UserId")]
    public virtual User User { get; set; }
}
