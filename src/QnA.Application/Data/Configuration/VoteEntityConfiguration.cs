namespace QnA.Application.Data.Configuration;

public class VoteEntityConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder
            .HasOne(v => v.Answer)
            .WithMany(a => a.Votes)
            .HasForeignKey(v => v.AnswerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(v => v.User)
            .WithMany(u => u.Votes)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Navigation(v => v.User)
            .AutoInclude();

        builder
            .HasData(new[]
            {
                 new Vote()
                {
                     Id = 1,
                     AnswerId =1,
                    IsUpVote = true,
                    UserId = 3
                },
                new Vote()
                {
                    Id = 2,
                    AnswerId =1,
                    IsUpVote = false,
                    UserId = 4
                },
                new Vote()
                {
                    Id = 3,
                    AnswerId = 2,
                    IsUpVote = true,
                    UserId = 2
                },
                new Vote()
                {
                    Id = 4,
                    AnswerId =2,
                    IsUpVote = true,
                    UserId = 1
                },
            });
    }
}
