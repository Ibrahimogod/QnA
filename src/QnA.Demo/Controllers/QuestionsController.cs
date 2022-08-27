namespace QnA.Demo.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public QuestionsController(IMediator mediator,ILogger<QuestionsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var model = await _mediator.Send(new GetAllQuestions(), cancellationToken);
        return Ok(model);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get Question By Id", id);

        var model = await _mediator.Send(new GetQuestionById(id), cancellationToken);

        if (model is null)
            return NotFound();

        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddQuestionModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var saved = await _mediator.Send(new AddQuestion(model.Content, GetUserId()), cancellationToken);

        return CommandResult(saved);
    }

    [HttpPost("{id}/answers")]
    public async Task<IActionResult> Post(int id, AddQuestionAnswerModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var saved = await _mediator.Send(new AddQuestionAnswer(id, GetUserId(), model.Answer), cancellationToken);

        return CommandResult(saved);
    }

    [HttpPost("{id}​/answers​/{answerId}​/votes")]
    public async Task<IActionResult> Post(int id, int answerId, AddVoteModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var voteAdded = await _mediator.Send(new AddVote(id, answerId, GetUserId(), model.IsUpVote), cancellationToken);

        return CommandResult(voteAdded);
    }

    [HttpDelete("{id}/answers​/{answerId}")]
    public async Task<IActionResult> Delete(int id, int answerId, CancellationToken cancellationToken)
    {
        var deleted = await _mediator.Send(new DeleteAsnswer(answerId, id, GetUserId()), cancellationToken);

        return CommandResult(deleted);

    }

    #region Helpers
    private int GetUserId()
    {
        return int.Parse(User.FindFirstValue(JwtClaimTypes.Id));
    }

    private IActionResult CommandResult(bool saved)
    {
        if (!saved)
            return BadRequest();

        return Ok();
    }
    #endregion
}
