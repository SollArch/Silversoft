using Core.Entities.Abstract;

namespace Entities.Concrete;

public class AdminPassword : IEntity
{
    public int Id { get; set; }
    public string Password { get; set; }
}