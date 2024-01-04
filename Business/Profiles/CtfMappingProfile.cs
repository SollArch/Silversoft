using AutoMapper;
using Entities.Concrete;
using Entities.DTO.Get;
using Entities.DTO.Post.Ctf;

namespace Business.Profiles;

public class CtfMappingProfile : Profile
{
    public CtfMappingProfile()
    {
        CreateMap<CtfAddDto, Ctf>().ReverseMap();
        CreateMap<CtfUpdateDto, Ctf>().ReverseMap();
        CreateMap<CheckAnswerDto, UserPoint>().ReverseMap();
        CreateMap<CtfGetDto, Ctf>().ReverseMap();
    }
}