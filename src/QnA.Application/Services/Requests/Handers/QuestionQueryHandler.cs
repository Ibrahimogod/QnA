namespace QnA.Application.Services.Requests.Handers;

public class QuestionQueryHandler : IRequestHandler<GetQuestionById, QuestionModel>, IRequestHandler<GetAllQuestions, IEnumerable<QuestionOverviewModel>>
{
    private readonly IQuestionService _questionService;

    public QuestionQueryHandler(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    public async Task<QuestionModel> Handle(GetQuestionById request, CancellationToken cancellationToken)
    {
        var question = await _questionService.GetQuestionByIdAsync(request.QuestionId, cancellationToken);

        if (question is null)
            return null;
        var model = new QuestionModel()
        {
            Id = question.Id,
            Content = question.Content,
            Username = question.User.UserName,
            Answers = question.Answers.Select(answer => new AnswerModel()
            {
                Id = answer.Id,
                Content = answer.Content,
                Votes = answer.Votes.Select(vote => new VoteModel()
                {
                    Id = vote.Id,
                    IsUpVote = vote.IsUpVote
                }),
                UpvotesCount = answer.Votes.Where(vote => vote.IsUpVote).Count(),
                IsDownVoted = answer.Votes.Where(vote => vote.IsUpVote).Count() < answer.Votes.Where(vote => !vote.IsUpVote).Count(),

            }),
        };

        return CalculateQuestionRank(model);
    }
    
    private QuestionModel CalculateQuestionRank(QuestionModel model)
    {
        var maxUpVotes = model.Answers.Sum(a => a.UpvotesCount);
        var answersCount = model.Answers.Count();
        var countOfDownVotedAnswers = model.Answers.Count(a => a.IsDownVoted);
        model.Rank = (maxUpVotes * answersCount) - countOfDownVotedAnswers;
        return model;
    }

    public async Task<IEnumerable<QuestionOverviewModel>> Handle(GetAllQuestions request, CancellationToken cancellationToken)
    {
        var questions = await _questionService.GetQuestionsAsync();

        return await Task.FromResult(questions.Select(question => new QuestionOverviewModel()
        {
            Id = question.Id,
            Content = question.Content
        }));
    }
}
