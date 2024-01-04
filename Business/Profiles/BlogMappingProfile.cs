using AutoMapper;
using Entities.Concrete;
using Entities.DTO.Post.Blog;

namespace Business.Profiles;

public class BlogMappingProfile : Profile
{
    public BlogMappingProfile()
    {
        CreateMap<Blog, BlogUpdateDto>().ReverseMap();
        CreateMap<Blog, BlogAddDto>().ReverseMap();
    }
}