using lunch.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace lunch.Repositories.Migrations
{
    public class ApplicationDbContextFactory : IDbContextFactory<DbContext>
    {
        public DbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
