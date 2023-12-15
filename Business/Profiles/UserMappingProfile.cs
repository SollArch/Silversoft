using AutoMapper;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTO.Get;
using Entities.DTO.Post.User;

namespace Business.Profiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserGetDto>().ReverseMap();
        CreateMap<User, UserForUpdate>().ReverseMap();
        CreateMap<User, UserForDelete>().ReverseMap();

    }
}