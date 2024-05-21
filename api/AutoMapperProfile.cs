using AutoMapper;

namespace api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserRegisterRequestDto, User>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Passoword));
        CreateMap<User, UserRegisterResponseDto>();
    }
}
