using Core.Entities.Abstract;

namespace Entities.DTO.Get;

public class UserGetDto : IDto
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string StudentNumber { get; set; } 
    public int Point { get; set; }
}