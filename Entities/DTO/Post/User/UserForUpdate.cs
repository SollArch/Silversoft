using System;
using Core.Entities.Abstract;

namespace Entities.DTO.Post.User;

public class UserForUpdate : IDto
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string StudentNumber { get; set; } 
}