using System;
using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace Entities.DTO.Post.BlogImage;

public class BlogImageAddDto : IDto
{
    public Guid BlogId { get; set; }
    public IFormFile FormFile { get; set; }
}