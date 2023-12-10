using System;
using Core.Entities.Abstract;

namespace Entities.Concrete
{
    public class Otp : IEntity
    {
        public int OtpId { get; set; }
        public string UserName { get; set; }
        public string OneTimePassword { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}