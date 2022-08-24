namespace Microsoft.AspNetCore.Builder;


public static class DataSeedExtentions
{
    public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
    {

        using (var scope = app.ApplicationServices.CreateScope())
        {

            var db = scope.ServiceProvider.GetRequiredService<QnADbContext>();
            if (!db.Users.Any())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                var user1 = new User() { Email = "test1@gmail.com", UserName = "Firsttestuser" };
                var user2 = new User() { Email = "test2@gmail.com", UserName = "Secondtestuser" };
                var user3 = new User() { Email = "test3@gmail.com", UserName = "Thirdtestuser" };
                var user4 = new User() { Email = "test4@gmail.com", UserName = "Fourthtestuser" };

                await userManager.CreateAsync(user1, "Test1@QnA");
                await userManager.CreateAsync(user2, "Test2@QnA");
                await userManager.CreateAsync(user3, "Test3@QnA");
                await userManager.CreateAsync(user4, "Test4@QnA");

                #region Question1
                var question1Answer1Vote1 = new Vote()
                {
                    IsUpVote = true,
                    UserId = user3.Id
                };


                var question1Answer1Vote2 = new Vote()
                {
                    IsUpVote = false,
                    UserId = user4.Id
                };

                var question1Answer1 = new Answer()
                {
                    Content = "Windows",
                    UserId = user2.Id,
                    Votes = new[]{ question1Answer1Vote1, question1Answer1Vote2 }
                };

                var question1 = new Question()
                {
                    Content = "What OS do you Use?",
                    UserId = user1.Id,
                    Answers = new[] {  question1Answer1 }
                };
                #endregion

                #region Question2

                var question2Answer1Vote1 = new Vote()
                {
                    IsUpVote = true,
                    UserId = user2.Id
                };

                var question2Answer1Vote2 = new Vote()
                {
                    IsUpVote = true,
                    UserId = user1.Id
                };



                var question2Answer1 = new Answer()
                {
                    Content = "No I Prefare Docker",
                    UserId = user3.Id,
                    Votes = new[] { question2Answer1Vote1,question2Answer1Vote2 }
                };

                var question2 = new Question()
                {
                    Content = "Can u use virtual machines?",
                    UserId = user4.Id,
                    Answers = new[] { question2Answer1 }
                };
                #endregion

                await db.Questions.AddRangeAsync(question1, question2);
                await db.SaveChangesAsync();

            }
        }


        return app;
    }
}

