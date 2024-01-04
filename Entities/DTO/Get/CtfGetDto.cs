using System;

namespace Entities.DTO.Get;

public class CtfGetDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Question { get; set; }
    public int Point { get; set; }
    public int SolvabilityLimit { get; set; }
    public int NumberOfSolve { get; set; }
    public int HintCount { get; set; }
    public bool IsActive { get; set; }
}