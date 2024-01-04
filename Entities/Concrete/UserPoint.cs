using System;
using Core.Entities.Abstract;

namespace Entities.Concrete;

public class UserPoint : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int Point { get; set; }
}