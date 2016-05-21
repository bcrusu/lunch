using lunch.Domain.Security;
using Microsoft.EntityFrameworkCore;

namespace lunch.Repositories.Impl
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
    }
}
