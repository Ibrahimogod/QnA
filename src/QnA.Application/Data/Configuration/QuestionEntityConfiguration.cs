namespace QnA.Application.Data.Configuration;

public class QuestionEntityConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder
            .HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(q => q.User)
            .WithMany(u => u.Questions)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Navigation(q => q.Answers)
            .AutoInclude();

        builder
            .Navigation(q => q.User)
            .AutoInclude();

        builder.HasData(new[]
        {
            new Question()
                {
                    Id=1,
                    Content = "What OS do you Use?",
                    UserId = 1,
                },
            new Question()
                {
                    Id = 2,
                    Content = "Can u use virtual machines?",
                    UserId = 4,
                }
        });
    }
}
