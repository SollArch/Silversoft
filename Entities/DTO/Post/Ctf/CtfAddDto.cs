using Core.Entities.Abstract;

namespace Entities.DTO.Post.Ctf;

public class CtfAddDto : IDto
{
    public string Title{ get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Hint { get; set; }
    public int Point { get; set; }
    public int SolvabilityLimit { get; set; }
}