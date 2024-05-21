namespace api;

public interface IAuthService
{
    Task<UserResponseDto> AddUser(UserRequestDto userRequestDto);
}
