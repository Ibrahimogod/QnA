
namespace QnA.Application.Services;

public class QuestionService : IQuestionService
{
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Answer> _answerRepository;
    private readonly IRepository<Vote> _voteRepository;
    private readonly IMemoryCache _memoryCache;

    public QuestionService(IRepository<Question> questionRepository, IRepository<Answer> answerRepository, IRepository<Vote> voteRepository, IMemoryCache memoryCache)
    {
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
        _voteRepository = voteRepository;
        _memoryCache = memoryCache;
    }

    public async Task<bool> AddQuestionAsync(Question question, CancellationToken cancellationToken)
    {
        await _questionRepository.InsertAsync(question, cancellationToken);

        return await _questionRepository.SaveAsync(cancellationToken) > 0;
    }

    public async Task<bool> AddQuestionAnswerAsync(Answer answer, CancellationToken cancellationToken)
    {
        await _answerRepository.InsertAsync(answer, cancellationToken);

        return await _answerRepository.SaveAsync(cancellationToken) > 0;
    }

    public async Task<Answer> GetAnswerByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _answerRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> DeleteAnswerAsync(Answer answer, CancellationToken cancellationToken)
    {
        await _answerRepository.DeleteAsync(answer);

        return await _answerRepository.SaveAsync(cancellationToken) > 0;
    }

    public async Task<bool> AddAnswerVoteAsync(Vote vote, CancellationToken cancellationToken)
    {
        await _voteRepository.InsertAsync(vote, cancellationToken);

        return await _voteRepository.SaveAsync(cancellationToken) > 0;
    }

    public async Task<IQueryable<Question>> GetQuestionsAsync()
    {
        return await _memoryCache.GetOrCreateAsync(CacheKeyHelpers.GET_ALL_QUESTIONS, async cacheEntry =>
        {
            CacheKeyHelpers.SetCacheEntry(cacheEntry);

            return await Task.FromResult(_questionRepository.GetAll());
        });
    }

    public async Task<Question> GetQuestionByIdAsync(int id,CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(string.Format(CacheKeyHelpers.GET_QUESTION_BY_ID, id), async cacheEntry =>
        {
            CacheKeyHelpers.SetCacheEntry(cacheEntry);
            return await _questionRepository.GetByIdAsync(id, cancellationToken);
        });
    }
}
