namespace QnA.Core.Domains.Users;

public class User : IdentityUser<int>, IEntity
{
    public virtual ICollection<Question> Questions { get; set; }

    public virtual ICollection<Answer> Answers { get; set; }

    public virtual ICollection<Vote> Votes { get; set; }
}
