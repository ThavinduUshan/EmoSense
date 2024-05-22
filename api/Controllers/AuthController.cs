using Microsoft.AspNetCore.Mvc;

namespace api;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserRegisterResponseDto>> Register(UserRegisterRequestDto dto)
    {
        var registeredUser = await _authService.RegisterUser(dto);
        return Ok(registeredUser);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserLoginResponseDto>> Login(UserLoginRequestDto dto)
    {
        var loggedUser = await _authService.LoginUser(dto);
        if (loggedUser == null)
        {
            return BadRequest("Invalid Credentials");
        }
        return Ok(loggedUser);
    }
}
