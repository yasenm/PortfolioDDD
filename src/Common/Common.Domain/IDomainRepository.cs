using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Common.Domain
{
    public interface IDomainRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        Task<TEntity> SaveAsync(TEntity entity, CancellationToken token = default);
    }
}
