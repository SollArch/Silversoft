using System;
using Core.Entities.Abstract;

namespace Entities.DTO.Post.Blog;

public class BlogLikeDto : IDto
{
    public Guid Id { get; set; }
}