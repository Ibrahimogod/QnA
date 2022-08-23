namespace QnA.Application.Services.Requests.Handers;

public class AccountCommandHandler : IRequestHandler<Login, AccessTokenResponse>, IRequestHandler<Register, IdentityResult>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtOptions _jwtOptions;

    public AccountCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtOptions> options)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtOptions = options.Value;
    }

    public async Task<AccessTokenResponse> Handle(Login request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValid)
            return null;

        var claims = await _signInManager.CreateUserPrincipalAsync(user);

        return await GenerateAccessTokenAsync(claims.Claims);
    }

    public async Task<IdentityResult> Handle(Register request, CancellationToken cancellationToken)
    {
        return await _userManager.CreateAsync(new User()
        {
            Email = request.Email,
            UserName = request.Username,
        }, request.Password);
    }

    #region Helpers
    private async Task<AccessTokenResponse> GenerateAccessTokenAsync(IEnumerable<Claim> claims)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var issuer = _jwtOptions.Issuer;
        var audience = _jwtOptions.Audience;
        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials);

        var jwtHandler = new JwtSecurityTokenHandler();

        var encodedToken = jwtHandler.WriteToken(token);

        return await Task.FromResult(new AccessTokenResponse
        {
            access_token = encodedToken,
            expires_in = token.Payload.Exp.Value
        });
    }
    #endregion
}
