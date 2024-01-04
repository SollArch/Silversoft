using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            connectionString: @"Server=MFBILGIN\MFBILGIN;Database=silversoft;Trusted_Connection=true;TrustServerCertificate=True;");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<BlogImage> BlogImages { get; set; }
    public DbSet<Ctf> Ctfs { get; set; }
    public DbSet<CtfQuestionImage> CtfQuestionImages { get; set; }
    public DbSet<CtfSolve> CtfSolves { get; set; }
    public DbSet<UserPoint> UserPoints { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Prize> Prizes { get; set; }
    public DbSet<Otp> Otps { get; set; }
    public DbSet<AdminPassword> AdminPasswords { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<CloudinaryConnection> CloudinaryConnections { get; set; } }