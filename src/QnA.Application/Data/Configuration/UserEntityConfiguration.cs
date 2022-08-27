namespace QnA.Application.Data.Configuration;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasData(new[]
            {
                 new User()
                 {
                     Id = 1,
                     Email = "test1@gmail.com",
                     UserName = "Firsttestuser",
                     NormalizedUserName = "FIRSTTESTUSER",
                     NormalizedEmail = "TEST1@GMAIL.COM",
                     PasswordHash = "AQAAAAEAACcQAAAAEOJAIav4yCyfFqnVcrIkzTjZvWpymnwhrVOMToAoeShcMw3pV2O6xDPsEvtZTAi5Hw==",
                     SecurityStamp = "5NY533KA5J5NRFHUOR5MS3EPLIX2HC4O",
                     ConcurrencyStamp ="82dcc839-270d-4cdb-bf88-e10ede7f7c72",
                     LockoutEnabled = true,
                 },
                new User()
                {
                     Id = 2,
                     UserName = "Secondtestuser",
                     NormalizedUserName = "SECONDTESTUSER",
                     Email = "test2@gmail.com",
                     NormalizedEmail = "TEST2@GMAIL.COM",
                     PasswordHash = "AQAAAAEAACcQAAAAECckpVwefhV6DgSN2IzcMYLTcEJnd7qTYhdUAFgLVlrXrE+l6MVeVcbn8Zdw5TzgAQ==",
                     SecurityStamp = "3JONSFU2FRIC6KJV4SWUQMEI5JU3TPHR",
                     ConcurrencyStamp ="63e8dc07-6388-4746-8e80-c0fd59bb93ca",
                     LockoutEnabled = true,
                 },
                new User()
                {
                     Id = 3,
                     UserName = "Thirdtestuser",
                     NormalizedUserName = "THIRDTESTUSER",
                     Email = "test3@gmail.com",
                     NormalizedEmail = "TEST3@GMAIL.COM",
                     PasswordHash = "AQAAAAEAACcQAAAAEOkAtjHCXG+MDv8WXm2XtNiJJs4lGf2ggXrP5mqEV0yV7dk35BcJumgFWp/5mLxI/w==",
                     SecurityStamp = "J5J5PQXL6MQ3IUHEQ6Y726VGX2XU2G53",
                     ConcurrencyStamp ="dbaa31ac-2a84-4c2b-9860-c2507d244eaf",
                     LockoutEnabled = true,
                 },
                new User()
                {
                     Id = 4,
                     UserName = "Fourthtestuser",
                     NormalizedUserName = "FOURTHTEaSTUSER",
                     Email = "test4@gmail.com",
                     NormalizedEmail = "TEST4@GMAIL.COM",
                     PasswordHash = "AQAAAAEAACcQAAAAELGQJ33UxMEk7eghhst33coZ6dkPPlHe+eDpLk1kcmwEw/yaRZ1JQRWFjTDW3GAdng==",
                     SecurityStamp = "UZFG2HO3EBKOEXKST47PR2RNHKO7PKYV",
                     ConcurrencyStamp ="c2d9829b-ae90-4e95-9808-b37d59418b39",
                     LockoutEnabled = true,
                 },

            });
    }
}
