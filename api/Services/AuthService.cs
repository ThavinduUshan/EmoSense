
using api.Migrations;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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

    public async Task<UserRegisterResponseDto> RegisterUser(UserRegisterRequestDto dto)
    {
        //Hashing the Password
        dto.Passoword = BCrypt.Net.BCrypt.HashPassword(dto.Passoword);

        //Mapping the request Dto to the user entity.
        var newUser = _mapper.Map<User>(dto);

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();

        var registeredUser = _mapper.Map<UserRegisterResponseDto>(newUser);
        return registeredUser;
    }

    public async Task<UserLoginResponseDto> LoginUser(UserLoginRequestDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
        if (user == null)
        {
            return null;
        }
        if(!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return null;
        }

        var loggedUser = _mapper.Map<UserLoginResponseDto>(user);

        return loggedUser;
    }
    
}
