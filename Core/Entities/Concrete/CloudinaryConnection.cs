using System;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete;

public class CloudinaryConnection : IEntity
{
    public Guid Id { get; set; }
    public string ApiKey { get; set; }
    public string ApiSecret { get; set; }
    public string Cloud { get; set; }
}