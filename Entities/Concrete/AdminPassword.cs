using System;
using Core.Entities.Abstract;

namespace Entities.Concrete;

public class AdminPassword : IEntity
{
    public Guid Id { get; set; }
    public string Password { get; set; }
}