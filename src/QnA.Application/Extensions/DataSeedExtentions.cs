namespace Microsoft.AspNetCore.Builder;


public static class DataSeedExtentions
{
    public static IApplicationBuilder UseDataSeeding(this IApplicationBuilder app)
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

                userManager.CreateAsync(user1, "Test1@QnA");
                userManager.CreateAsync(user2, "Test2@QnA");
                userManager.CreateAsync(user3, "Test3@QnA");
                userManager.CreateAsync(user4, "Test4@QnA");

                #region Question1
                var question1 = new Question()
                {
                    Content = "What OS do you Use?",
                    UserId = user1.Id,
                };

                var question1Answer1 = new Answer()
                {
                    Content = "Windows",
                    QuestionId = question1.Id,
                    UserId = user2.Id
                };


                var question1Answer1Vote1 = new Vote()
                {
                    AnswerId = question1Answer1.Id,
                    IsUpVote = true,
                    UserId = user3.Id
                };

                var question1Answer1Vote2 = new Vote()
                {
                    AnswerId = question1Answer1.Id,
                    IsUpVote = false,
                    UserId = user4.Id
                };

                #endregion

                #region Question2
                var question2 = new Question()
                {
                    Content = "Can u use virtual machines?",
                    UserId = user4.Id,
                };


                var question2Answer1 = new Answer()
                {
                    Content = "No I Prefare Docker",
                    QuestionId = question2.Id,
                    UserId = user3.Id
                };


                var question2Answer1Vote1 = new Vote()
                {
                    AnswerId = question2Answer1.Id,
                    IsUpVote = true,
                    UserId = user2.Id
                };

                var question2Answer1Vote2 = new Vote()
                {
                    AnswerId = question2Answer1.Id,
                    IsUpVote = true,
                    UserId = user1.Id
                };

                #endregion

                db.Questions.AddRange(question1, question2);
                db.Answers.AddRange(question1Answer1, question2Answer1);
                db.Votes.AddRangeAsync(question1Answer1Vote1, question1Answer1Vote2, question2Answer1Vote1, question2Answer1Vote2);

                db.SaveChanges();
            }
        }


        return app;
    }
}

