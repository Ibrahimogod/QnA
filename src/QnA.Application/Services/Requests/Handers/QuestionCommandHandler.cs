namespace QnA.Application.Services.Requests.Handers;

public class QuestionCommandHandler : IRequestHandler<AddQuestion, bool>, IRequestHandler<AddQuestionAnswer, bool>, IRequestHandler<DeleteAsnswer, bool>, IRequestHandler<AddVote, bool>
{
    private readonly IQuestionService _questionService;

    public QuestionCommandHandler(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    public async Task<bool> Handle(AddQuestion request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Content))
            return false;

        return await _questionService.AddQuestionAsync(new Question() { Content = request.Content, UserId = request.UserId }, cancellationToken);
    }

    public async Task<bool> Handle(AddQuestionAnswer request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Answer))
            return false;

        return await _questionService.AddQuestionAnswerAsync(new Answer()
        {
            Content = request.Answer,
            QuestionId = request.QuestionId,
            UserId = request.UserId
        }, cancellationToken);
    }

    public async Task<bool> Handle(DeleteAsnswer request, CancellationToken cancellationToken)
    {
        var answer = await _questionService.GetAnswerByIdAsync(request.AsnswerId, cancellationToken);

        if (answer is null || answer.UserId != request.UserId || answer.QuestionId != request.QuestionId)
            return false;

        return await _questionService.DeleteAnswerAsync(answer, cancellationToken);
    }

    public async Task<bool> Handle(AddVote request, CancellationToken cancellationToken)
    {
        var answer = await _questionService.GetAnswerByIdAsync(request.AnswerId, cancellationToken);

        if (answer.Votes.Any(vote => vote.UserId == request.UserId)
            || answer.UserId == request.UserId
            || request.IsUpVote is null)
            return false;

        return await _questionService.AddAnswerVoteAsync(new Vote()
        {
            AnswerId = request.AnswerId,
            UserId = request.UserId,
            IsUpVote = request.IsUpVote.Value
        }, cancellationToken);
    }
}