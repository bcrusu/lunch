using System.Data.Entity.Migrations;

namespace lunch.Repositories.Impl.Migrations
{
    internal class ApplicationDbMigrationsConfiguration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public ApplicationDbMigrationsConfiguration()
        {
            MigrationsDirectory = @"Impl\Migrations";
            MigrationsAssembly = typeof(ApplicationDbMigrationsConfiguration).Assembly;
            MigrationsNamespace = typeof(ApplicationDbMigrationsConfiguration).Namespace;

            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            
        }
    }
}
