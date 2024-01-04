using System;
using Core.Entities.Abstract;

namespace Entities.DTO.Post.User;

public class UserForDelete : IDto
{
    public Guid UserId { get; set; }
}