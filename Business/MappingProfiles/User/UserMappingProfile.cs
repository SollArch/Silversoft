using System.Collections.Generic;
using AutoMapper;
using Entities.DTO.Get;

namespace Business.MappingProfiles.User;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<Core.Entities.Concrete.User, UserGetDto>().ReverseMap();
        
    }
}