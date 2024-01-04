using System;
using Core.Entities.Abstract;

namespace Entities.Concrete;

public class Like : IEntity
{
    public Guid LikeId { get; set; }
    public Guid UserId { get; set; }
    public Guid BlogId { get; set; }
}