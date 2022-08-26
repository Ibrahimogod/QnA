namespace QnA.Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;

    public AccountController(IMediator mediator, ILogger<AccountController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [Route("Login")]
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _logger.LogInformation("User Login", model);

        var reponse = await _mediator.Send(new Login(model.Email, model.Password), cancellationToken);

        if (reponse is null)
            return Unauthorized();

        _logger.LogInformation("Success Login", reponse);
        return Ok(reponse);
    }

    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _logger.LogInformation("User Register", model);
        var result = await _mediator.Send(new Register(model.Email, model.Username, model.Password), cancellationToken);

        if (!result.Succeeded)
        {
            _logger.LogInformation("Invalid User Register", result);
            return IdentityError(result.Errors);
        }

        _logger.LogInformation("Success User Register", result);
        return Ok();
    }

    #region Helpers

    private IActionResult IdentityError(IEnumerable<IdentityError> errors)
    {
        foreach (var error in errors)
            ModelState.AddModelError(error.Code, error.Description);

        return BadRequest(ModelState);
    }

    #endregion
}
