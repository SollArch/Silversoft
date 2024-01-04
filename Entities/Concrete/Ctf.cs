using System;
using Core.Entities.Abstract;

namespace Entities.Concrete;

public class Ctf : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Hint { get; set; }
    public int Point { get; set; }
    public int SolvabilityLimit { get; set; }
    public int NumberOfSolve { get; set; }
    public bool IsActive { get; set; }
}