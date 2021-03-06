﻿using System.Data.Entity;
using lunch.Domain.Security;

namespace lunch.Repositories.Impl
{
    [DbConfigurationType(typeof(ApplicationDbConfiguration))]
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=lunch")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
