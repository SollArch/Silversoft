using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Otp : IEntity
    {
        public Guid OtpId { get; set; }
        public string Email { get; set; }
        public string OneTimePassword { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}