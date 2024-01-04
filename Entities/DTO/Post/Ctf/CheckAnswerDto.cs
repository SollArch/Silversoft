using System;

namespace Entities.DTO.Post.Ctf;

public class CheckAnswerDto
{
    public Guid CtfId { get; set; }
    public string Answer { get; set; }
}