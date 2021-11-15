namespace Portfolio.Infrastructure.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Portfolio.Application.Contracts;
    using Portfolio.Domain.Posts.Models.Posts;
    using Portfolio.Infrastructure.Persistence;
    using Portfolio.Infrastructure.Persistence.Models;
    using Portfolio.Infrastructure.Persistence.Repositories;
    using System.Reflection;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddMapping()
                .AddDbContextSetup(configuration)
                .AddRepositories();

        internal static IServiceCollection AddDbContextSetup(this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<PortfolioDbContext>(options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(PortfolioDbContext).Assembly.FullName)));

        internal static IServiceCollection AddTestInfrastructure(this IServiceCollection services)
            => services
                .AddMapping()
                .AddTestInMemoryDbContext()
                .AddRepositories();

        internal static IServiceCollection AddTestInMemoryDbContext(this IServiceCollection services)
            => services.AddDbContext<PortfolioDbContext>(options => options
                    .UseInMemoryDatabase("TestInMemory"));

        internal static IServiceCollection AddRepositories(this IServiceCollection services)
            => services
                .AddTransient<IDomainRepository<Post>, DataRepository<Post, PostData>>()
                .AddTransient<DataRepository<Post, PostData>, PostRepository>();

        internal static IServiceCollection AddMapping(this IServiceCollection services)
            => services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
