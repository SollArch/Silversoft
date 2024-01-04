using System;
using Core.Entities.Abstract;

namespace Entities.DTO.Post.User;

public class UserForPoint : IDto
{
    public Guid UserId { get; set; }
    public int Point { get; set; }
}