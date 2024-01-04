using System;
using Core.Entities.Abstract;

namespace Entities.Concrete;

public class BlogImage : IEntity
{
    public Guid Id { get; set; }
    public Guid BlogId { get; set; }
    public string ImagePath { get; set; }
}