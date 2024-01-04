using System;
using Core.Entities.Abstract;

namespace Entities.DTO.Post.Blog;

public class BlogUpdateDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
}