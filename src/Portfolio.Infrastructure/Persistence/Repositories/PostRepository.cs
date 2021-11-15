namespace Portfolio.Infrastructure.Persistence.Repositories
{
    using AutoMapper;
    using Portfolio.Domain.Posts.Models.Posts;
    using Portfolio.Infrastructure.Persistence.Models;

    internal class PostRepository : DataRepository<Post, PostData>
    {
        public PostRepository(PortfolioDbContext db, IMapper mapper) : base(db, mapper)
        {
        }
    }
}
