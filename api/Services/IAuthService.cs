namespace api;

public interface IAuthService
{
    Task<UserRegisterResponseDto> AddUser(UserRegisterRequestDto userRequestDto);
}
