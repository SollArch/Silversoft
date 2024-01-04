using AutoMapper;
using Entities.DTO.Post.Otp;

namespace Business.Profiles;

public class OtpMappingProfile : Profile
{
    public OtpMappingProfile()
    {
        CreateMap<CheckOtpDto, CheckOtpForChangePasswordDto>().ReverseMap();
    }
}