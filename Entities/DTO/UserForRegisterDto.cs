using Core.Entities.Abstract;

namespace Entities.DTO
{
    public abstract class UserForRegisterDto : IDto
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string StudentNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}