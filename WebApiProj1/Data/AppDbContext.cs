using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiProj1.Models.Entities;

namespace WebApiProj1.Data
{
    public class AppDbContext : IdentityDbContext<IdtyUser, Roles, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdtyUser>().ToTable("AspNetUsers");
            builder.Entity<Roles>().ToTable("AspNetRoles");
        }

        public DbSet<IdtyUser> Users { get; set; }
    }
}
