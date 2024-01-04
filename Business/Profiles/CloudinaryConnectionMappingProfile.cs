using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTO.Post.CloudinaryConnection;

namespace Business.Profiles;

public class CloudinaryConnectionMappingProfile : Profile
{
    public CloudinaryConnectionMappingProfile()
    {
        CreateMap<AddCloudinaryConnectionDto, CloudinaryConnection>().ReverseMap();
    }
}