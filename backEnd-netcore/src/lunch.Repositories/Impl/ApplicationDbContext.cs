using lunch.Configuration;
using lunch.Domain.Security;
using Microsoft.EntityFrameworkCore;

namespace lunch.Repositories.Impl
{
    internal class ApplicationDbContext : DbContext
    {
        private readonly IApplicationSettings _applicationSettings;

        public ApplicationDbContext(IApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(_applicationSettings.ConnectionString, x =>
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
