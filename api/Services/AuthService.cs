
using AutoMapper;

namespace api;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public AuthService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> AddUser(UserRequestDto userRequestDto)
    {
        //Hashing the Password
        userRequestDto.Passoword = BCrypt.Net.BCrypt.HashPassword(userRequestDto.Passoword);

        //Mapping the request Dto to the user entity.
        var newUser = _mapper.Map<User>(userRequestDto);

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        var registeredUser = _mapper.Map<UserResponseDto>(newUser);
        return registeredUser;
    }
}
