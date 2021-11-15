namespace Portfolio.Application.Contracts
{
    using Portfolio.Common.Domain;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDomainRepository<out TEntity>
        where TEntity : IAggregateRoot
    {
        IQueryable<TEntity> All();

        Task<int> SaveChanges(CancellationToken cancellationToken = default);
    }
}
