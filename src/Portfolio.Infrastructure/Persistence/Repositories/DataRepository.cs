namespace Portfolio.Infrastructure.Persistence.Repositories
{
    using AutoMapper;
    using Portfolio.Application.Contracts;
    using Portfolio.Common.Application.Mapping;
    using Portfolio.Common.Domain;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DataRepository<TEntity, TEntityData> : IDomainRepository<TEntity>
        where TEntity : class, IAggregateRoot
        where TEntityData : class, IMapFrom<TEntity>
    {
        private readonly PortfolioDbContext db;
        private readonly IMapper mapper;

        public DataRepository(PortfolioDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public IQueryable<TEntity> All()
        {
            var set = this.db.Set<TEntityData>();
            var result = this.mapper.ProjectTo<TEntity>(set);
            return result;
        }

        public Task<int> SaveChanges(CancellationToken cancellationToken = default)
            => this.db.SaveChangesAsync(cancellationToken);
    }
}
