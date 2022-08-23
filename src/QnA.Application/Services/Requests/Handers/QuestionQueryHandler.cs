using QnA.Application.Common;

namespace QnA.Application.Services.Requests.Handers;

public class QuestionQueryHandler : IRequestHandler<GetQuestionById, QuestionModel>, IRequestHandler<GetAllQuestions, IEnumerable<QuestionOverviewModel>>
{

    private readonly IRepository<Question> _questionRepository;
    private readonly IMemoryCache _memoryCache;

    public QuestionQueryHandler(IRepository<Question> questionRepository, IMemoryCache memoryCache)
    {
        _questionRepository = questionRepository;
        _memoryCache = memoryCache;
    }

    public async Task<QuestionModel> Handle(GetQuestionById request, CancellationToken cancellationToken)
    {
        var question = await _memoryCache.GetOrCreateAsync(string.Format(CacheKeyHelpers.GET_QUESTION_BY_ID, request.QuestionId), async cacheEntry =>
        {
            CacheKeyHelpers.SetCacheEntry(cacheEntry);
            return await _questionRepository.Table
             .AsNoTracking()
             .Include(q => q.Answers)
             .ThenInclude(a => a.Votes)
             .ThenInclude(v => v.User)
             .Include(q => q.User)
             .FirstOrDefaultAsync(q => q.Id == request.QuestionId);
        });

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
        var MaxUpVotes = model.Answers.Sum(a => a.UpvotesCount);
        var AnswersCount = model.Answers.Count();
        var CountOfDownVotedAnswers = model.Answers.Count(a => a.IsDownVoted);
        model.Rank = (MaxUpVotes * AnswersCount) - CountOfDownVotedAnswers;
        return model;
    }

    public async Task<IEnumerable<QuestionOverviewModel>> Handle(GetAllQuestions request, CancellationToken cancellationToken)
    {
        var questions = _memoryCache.GetOrCreate(CacheKeyHelpers.GET_ALL_QUESTIONS, cacheEntry =>
        {
            CacheKeyHelpers.SetCacheEntry(cacheEntry);

            return _questionRepository.Table.AsNoTracking();
        });

        return await Task.FromResult(questions.Select(question => new QuestionOverviewModel()
        {
            Id = question.Id,
            Content = question.Content
        }));
    }
}
