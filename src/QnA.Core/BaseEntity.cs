namespace QnA.Core;

public class BaseEntity : IEntity
{
    [Key]
    public int Id { get; set; }
}
