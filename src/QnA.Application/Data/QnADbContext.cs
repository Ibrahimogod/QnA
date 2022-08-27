
namespace QnA.Application.Data;

public class QnADbContext : IdentityDbContext<User,UserRole,int>
{
	public QnADbContext(DbContextOptions<QnADbContext> options): base(options)
	{
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(QnADbContext).Assembly);
    }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }
}
