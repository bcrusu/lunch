using System.Threading;
using System.Threading.Tasks;

namespace lunch.Repositories.Impl
{
    internal class ApplicationDbContextOperations : IDbContextOperations
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationDbContextOperations(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
