using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    public AuthService(DataContext context, IMapper mapper, IConfiguration config)
    {
        _context = context;
        _mapper = mapper;
        _config = config;
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
        var token = GenerateJWTToken(user);
        var loggedUser = _mapper.Map<UserLoginResponseDto>(user);
        loggedUser.Token = token;
        return loggedUser;
    }

    private string GenerateJWTToken(User user)
    {
        List<Claim> claims = new()
        {
            new Claim("Id", user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, Roles.User.ToString())
        };

        var key =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _config.GetSection("JwtConfig:Secret").Value
        ));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(
            claims : claims,
            expires : DateTime.Now.AddDays(1),
            signingCredentials : credentials
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;

    }
    
}
