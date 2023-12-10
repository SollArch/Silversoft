using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                connectionString: @"Server=MFBILGIN\MFBILGIN;Database=silversoft;Trusted_Connection=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Ctf> Ctfs { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Prize> Prizes { get; set; }
        public DbSet<Otp> Otps { get; set; }
    }
}