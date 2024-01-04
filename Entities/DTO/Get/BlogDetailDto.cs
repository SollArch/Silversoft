using System;
using System.Collections.Generic;

namespace Entities.DTO.Get;

public class BlogDetailDto
{
    public Guid Id { get; set; }
    public string AuthorUserName { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public int LikeCount { get; set; }
    public List<string> Likers { get; set; }
    public bool IsActive { get; set; }
}