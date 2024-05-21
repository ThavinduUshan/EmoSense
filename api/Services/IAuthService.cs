namespace api;

public interface IAuthService
{
    Task<UserRegisterResponseDto> RegisterUser(UserRegisterRequestDto dto);
    Task<UserLoginResponseDto> LoginUser(UserLoginRequestDto dto);
}
