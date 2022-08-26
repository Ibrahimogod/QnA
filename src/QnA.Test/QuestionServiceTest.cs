namespace QnA.Test;

public class QuestionServiceTest
{
    private static int _dbCount;

    public QuestionServiceTest()
    {
    }

    #region Setup
    //Sysytem Under test
    private IQuestionService SetupSUT()
    {
        QnADbContext qnaDbContext = SetupQnAContext();
        IRepository<Question> questionRepository = SetupQuestionRepository(qnaDbContext);
        IRepository<Answer> answerRepository = SetupAnswerRepository(qnaDbContext);
        IRepository<Vote> voteRepository = SetupVoteRepository(qnaDbContext);
        IMemoryCache memoryCache = SetupMemoryCache();

        return new QuestionService(questionRepository, answerRepository, voteRepository, memoryCache);
    }

    private IQuestionService SetupSUT(QnADbContext qnaDbContext)
    {
        IRepository<Question> questionRepository = SetupQuestionRepository(qnaDbContext);
        IRepository<Answer> answerRepository = SetupAnswerRepository(qnaDbContext);
        IRepository<Vote> voteRepository = SetupVoteRepository(qnaDbContext);
        IMemoryCache memoryCache = SetupMemoryCache();

        return new QuestionService(questionRepository, answerRepository, voteRepository, memoryCache);
    }

    private QnADbContext SetupQnAContext()
    {
        DbContextOptionsBuilder<QnADbContext> optionsBuilder = new DbContextOptionsBuilder<QnADbContext>();
        optionsBuilder.UseInMemoryDatabase($"QnADb{++_dbCount}");
        var options = optionsBuilder.Options;


        var db = new QnADbContext(options);

        var user1 = new User() { Email = "test1@gmail.com", UserName = "Firsttestuser" };
        var user2 = new User() { Email = "test2@gmail.com", UserName = "Secondtestuser" };
        var user3 = new User() { Email = "test3@gmail.com", UserName = "Thirdtestuser" };
        var user4 = new User() { Email = "test4@gmail.com", UserName = "Fourthtestuser" };

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

        db.Users.AddRange(user1, user2, user3, user4);
        db.Questions.AddRange(question1, question2);
        db.Answers.AddRange(question1Answer1, question2Answer1);
        db.Votes.AddRange(question1Answer1Vote1, question1Answer1Vote2, question2Answer1Vote1, question2Answer1Vote2);

        db.SaveChanges();
        return db;
    }

    private IRepository<Question> SetupQuestionRepository(QnADbContext qnaDbContext)
    {
        return new QuestionRepository(qnaDbContext);
    }

    public IRepository<Answer> SetupAnswerRepository(QnADbContext qnaDbContext)
    {
        return new AnswerRepository(qnaDbContext);
    }

    public IRepository<Vote> SetupVoteRepository(QnADbContext qnaDbContext)
    {
        return new VoteRepository(qnaDbContext);
    }

    public IMemoryCache SetupMemoryCache()
    {
        MemoryCacheOptions options = new MemoryCacheOptions();
        return new MemoryCache(options);
    }
    #endregion

    [Fact]
    public async void Givin_Question_When_DatabaseHas_2_Questions_Then_Returns_True()
    {
        //Arrange
        var sut = SetupSUT();
        var cancellationTokenSource = new CancellationTokenSource();

        var question = new Question()
        {
            Content = "Question DatabaseHas_2_Questions"
        };
        //Act
        var result = await sut.AddQuestionAsync(question, cancellationTokenSource.Token);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async void Givin_Question_With_Id_1_When_DatabaseHas_2_Questions_Then_Throws_InvalidOperationException()
    {
        //Arrange
        var sut = SetupSUT();
        var cancellationTokenSource = new CancellationTokenSource();

        var question = new Question()
        {
            Id = 1,
            Content = "Question DatabaseHas_2_Questions"
        };

        //Act //Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.AddQuestionAsync(question, cancellationTokenSource.Token));
    }


    [Fact]
    public async void Givin_Answer_When_DatabaseHas_2_Answers_Then_Returns_True()
    {
        //Arrange
        var sut = SetupSUT();
        var cancellationTokenSource = new CancellationTokenSource();
        var answer = new Answer()
        {
            Content = "Question DatabaseHas_2_Answers",
            QuestionId = 1
        };

        //Act
        var result = await sut.AddQuestionAnswerAsync(answer, cancellationTokenSource.Token);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public async void Givin_Answer_With_Id_1_When_DatabaseHas_2_Answers_Then_Throws_InvalidOperationException()
    {
        //Arrange
        var sut = SetupSUT();
        var cancellationTokenSource = new CancellationTokenSource();
        var answer = new Answer()
        {
            Id = 1,
            Content = "Question DatabaseHas_2_Answers",
            QuestionId = 1
        };

        //Act //Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await sut.AddQuestionAnswerAsync(answer, cancellationTokenSource.Token));
    }

    [Fact]
    public async void Givin_Answer_Id_2_When_Db_Has_2_Answers_Then_Returns_Answer_With_Specifed_Content()
    {
        //Arrange
        var sut = SetupSUT();
        var cancellationTokenSource = new CancellationTokenSource();
        var id = 2;
        var content = "No I Prefare Docker";

        //Act
        var result = await sut.GetAnswerByIdAsync(id, cancellationTokenSource.Token);

        //Assert
        Assert.Equal(content, result.Content);
    }

    [Fact]
    public async void Givin_Answer_Id_2_When_Db_Has_Answers_Count_n_Then_Returns_True_And_Answers_Count_Less_By_1()
    {
        //Arrange
        var qnaDbContext = SetupQnAContext();
        var sut = SetupSUT(qnaDbContext);
        var cancellationTokenSource = new CancellationTokenSource();
        var id = 2;
        var answer3Content = "No I Prefare Docker";

        var answer = await sut.GetAnswerByIdAsync(id, cancellationTokenSource.Token);

        //Act
        var n = await qnaDbContext.Answers.CountAsync(cancellationTokenSource.Token);
        var result = await sut.DeleteAnswerAsync(answer, cancellationTokenSource.Token);

        //Asserrt
        Assert.Equal(answer3Content, answer.Content);
        Assert.True(result);
        var answersCountAfter = qnaDbContext.Answers.Count();
        Assert.Equal(n - 1, answersCountAfter);
    }

    [Fact]
    public async void Givin_Vote_When_Db_Votes_Count_4_Then_Returns_True_And_Votes_Count_5()
    {
        //Arrange
        var qnaDbContext = SetupQnAContext();
        var sut = SetupSUT(qnaDbContext);
        var cancellationTokenSource = new CancellationTokenSource();
        var vote = new Vote()
        {
            AnswerId = 2,
            IsUpVote = false,
            UserId = 1
        };

        //Act
        var result = await sut.AddAnswerVoteAsync(vote, cancellationTokenSource.Token);
        var votesCountAfter = await qnaDbContext.Votes.CountAsync();

        //Asserrt
        Assert.True(result);
        Assert.Equal(5, votesCountAfter);
    }

    [Fact]
    public async void Givin___When_Db_Has_Count_2_Questions_Then_Returns_2()
    {
        //Arrange
        var qnaDbContext = SetupQnAContext();
        var sut = SetupSUT(qnaDbContext);
        var cancellationTokenSource = new CancellationTokenSource();
        var questionCount = await qnaDbContext.Questions.CountAsync(cancellationTokenSource.Token);

        //Act
        var result = await sut.GetQuestionsAsync();

        //Assert
        Assert.Equal(questionCount, result.Count());
    }

    [Fact]
    public async void Givin_Question_Id_2_When_Db_Has_2_Questions_Then_Returns_Question_With_Specified_Content()
    {
        //Arrange
        var qnaDbContext = SetupQnAContext();
        var sut = SetupSUT(qnaDbContext);
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        var id = 2;
        var content = "Can u use virtual machines?";

        //Act
        var result = await sut.GetQuestionByIdAsync(id, cancellationTokenSource.Token);

        //Assrert
        Assert.NotNull(result);
        Assert.Equal(content, result.Content);
    }
}