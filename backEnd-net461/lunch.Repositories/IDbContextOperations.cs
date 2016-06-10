using System.Threading;
using System.Threading.Tasks;

namespace lunch.Repositories
{
    public interface IDbContextOperations
    {
        void SaveChanges();

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
