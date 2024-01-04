using System;
using Core.Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace Entities.DTO.Post.CtfImage;

public class CtfQuestionImageAddDto : IDto
{
    public Guid CtfId { get; set; }
    public Guid UserId { get; set; }
    public IFormFile FormFile { get; set; }
}