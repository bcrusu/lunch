using lunch.Domain.Security;
using Microsoft.EntityFrameworkCore;

namespace lunch.Repositories.Impl
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("name=lunch", x =>
            {
                x.MigrationsAssembly("lunch.Repositories.Migrations");
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(x => x.Email)
                .IsUnique();
        }
    }
}
