using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; } // UNIQUE
        public string StudentNumber { get; set; } //UNIQUE
        public string Email { get; set; } //UNIQUE
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public int Point { get; set; }
        public bool Status { get; set; }
    }
}
