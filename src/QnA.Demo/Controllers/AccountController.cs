namespace QnA.Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("Login")]
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var reponse = await _mediator.Send(new Login(model.Email,model.Password), cancellationToken);

        if (reponse is null)
            return Unauthorized();

        return Ok(reponse);
    }

    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(new Register(model.Email,model.Username,model.Password), cancellationToken);

        if (!result.Succeeded)
            return IdentityError(result.Errors);

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
