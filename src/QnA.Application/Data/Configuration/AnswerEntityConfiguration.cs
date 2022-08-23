namespace QnA.Application.Data.Configuration;

public class AnswerEntityConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(a => a.User)
            .WithMany(u => u.Answers)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(a => a.Votes)
            .WithOne(v => v.Answer)
            .HasForeignKey(v => v.AnswerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
