namespace QnA.Application.Data.Configuration;

public class AnswerEntityConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder
            .HasOne(a => a.Question)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

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

        builder
            .Navigation(a => a.User)
            .AutoInclude();

        builder
            .Navigation(a => a.Votes)
            .AutoInclude();

        builder
            .HasData(new[]
            {
                new Answer()
                    {
                        Id = 1,
                        QuestionId = 1,
                        Content = "Windows",
                        UserId = 2,
                    },
                new Answer()
                    {
                        Id =2,
                        QuestionId = 2,
                        Content = "No I Prefare Docker",
                        UserId = 3,
                    }
            });
    }
}
