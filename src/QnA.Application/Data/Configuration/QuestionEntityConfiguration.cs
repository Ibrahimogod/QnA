namespace QnA.Application.Data.Configuration;

public class QuestionEntityConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder
            .HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(q => q.User)
            .WithMany(u => u.Questions)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
