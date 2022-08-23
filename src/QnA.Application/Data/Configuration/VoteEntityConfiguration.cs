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
            .OnDelete(DeleteBehavior.NoAction);
    }
}
