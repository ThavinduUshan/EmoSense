﻿using System.Security.Claims;
using AutoMapper;

namespace api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserRegisterRequestDto, User>()
        .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Passoword))
        .ForMember(dest => dest.UserRoleId, opt => opt.MapFrom(src => Roles.User));
        CreateMap<User, UserRegisterResponseDto>();
        CreateMap<User, UserLoginResponseDto>().ForMember(dest => dest.Token, opt => opt.Ignore());    
        CreateMap<MoodEntry, MoodEntryResponseDto>();
        CreateMap<MoodEntryRequestDto, MoodEntry>();
    }
}
