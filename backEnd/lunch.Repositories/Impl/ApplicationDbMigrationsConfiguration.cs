using System.Data.Entity.Migrations;

namespace lunch.Repositories.Impl
{
    internal class ApplicationDbMigrationsConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public ApplicationDbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
