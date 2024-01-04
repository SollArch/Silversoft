using System;
using Core.Entities.Abstract;

namespace Entities.DTO.Post.Ctf;

public class CtfUpdateDto : IDto
{
    public Guid Id { get; set; }
    public string Title{ get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Hint { get; set; }
    public int Point { get; set; }
    public int SolvabilityLimit { get; set; }
}