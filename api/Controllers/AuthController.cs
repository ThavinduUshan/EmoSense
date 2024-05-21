using Microsoft.AspNetCore.Mvc;

namespace api;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("register")]
    public async Task<ActionResult<UserRegisterResponseDto>> Add(UserRegisterRequestDto dto)
    {
        var registeredUser = await _authService.AddUser(dto);
        return Ok(registeredUser);
    }
}
