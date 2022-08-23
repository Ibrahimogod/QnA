namespace QnA.Application.Services.Requests.Handers;

public class QuestionCommandHandler : IRequestHandler<AddQuestion, bool>, IRequestHandler<AddQuestionAnswer, bool>, IRequestHandler<DeleteAsnswer, bool>, IRequestHandler<AddVote,bool>
{

    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<Answer> _answerRepository;
    private readonly IRepository<Vote> _voteRepository;

    public QuestionCommandHandler(IRepository<Question> questionRepository, IRepository<Answer> answerRepository,IRepository<Vote> voteRepository)
    {
        _questionRepository = questionRepository;
        _answerRepository = answerRepository;
        _voteRepository = voteRepository;
    }

    public async Task<bool> Handle(AddQuestion request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Content))
            return false;
        await _questionRepository.InsertAsync(new Question() { Content = request.Content, UserId = request.UserId }, cancellationToken);

        return await _questionRepository.SaveAsync(cancellationToken) > 0;
    }

    public async Task<bool> Handle(AddQuestionAnswer request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Answer))
            return false;

        await _answerRepository.InsertAsync(new Answer()
        {
            Content = request.Answer,
            QuestionId = request.QuestionId,
            UserId = request.UserId
        }, cancellationToken);

        return await _answerRepository.SaveAsync(cancellationToken) > 0;
    }

    public async Task<bool> Handle(DeleteAsnswer request, CancellationToken cancellationToken)
    {
        var answer = await _answerRepository.Table.FirstOrDefaultAsync(answer => answer.Id == request.AsnswerId, cancellationToken);
        
        if (answer is null || answer.UserId != request.UserId || answer.QuestionId != request.QuestionId)
            return false;
        await _answerRepository.DeleteAsync(answer);
        return await _answerRepository.SaveAsync(cancellationToken) > 0;

    }

    public async Task<bool> Handle(AddVote request, CancellationToken cancellationToken)
    {
        var answer = await _answerRepository.Table
            .AsNoTracking()
            .Include(answer => answer.User)
            .Include(answer => answer.Votes)
            .ThenInclude(v => v.User)
            .FirstOrDefaultAsync(answer => answer.Id == request.AnswerId);

        if (answer.Votes.Any(vote => vote.UserId == request.UserId) 
            || answer.UserId == request.UserId
            || request.IsUpVote is null)
            return false;

        await _voteRepository.InsertAsync(new Vote()
        {
            AnswerId = request.AnswerId,
            UserId = request.UserId,
            IsUpVote = request.IsUpVote.Value
        }, cancellationToken);

       return await _voteRepository.SaveAsync(cancellationToken) > 0;
    }
}
