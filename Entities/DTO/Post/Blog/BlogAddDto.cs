using Core.Entities.Abstract;

namespace Entities.DTO.Post.Blog;

public class BlogAddDto : IDto
{
    public string Title { get; set; }
    public string Content { get; set; }
}