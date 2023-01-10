using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TmeritFX.Models;

namespace TmeritFX.DataBase_Connection
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<UserVerification> UserVerifications { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
    }
}
