using System;
using Core.Entities.Abstract;

namespace Entities.Concrete;

public class CtfSolve : IEntity
{
    public Guid Id { get; set; }
    public Guid CtfId { get; set; }
    public Guid UserId { get; set; }
}