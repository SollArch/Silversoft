using Core.Entities.Abstract;

namespace Entities.DTO.Post.User;

public class UserForDelete : IDto
{
    public int UserId { get; set; }
}