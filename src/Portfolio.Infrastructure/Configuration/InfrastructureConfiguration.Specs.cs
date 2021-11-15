namespace Portfolio.Infrastructure.Configuration
{
    using Common.Infrastructure.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Portfolio.Application.Contracts;
    using Portfolio.Domain.Posts.Models.Posts;
    using Portfolio.Infrastructure.Persistence.Models;
    using Portfolio.Infrastructure.Persistence.Repositories;
    using Xunit;

    public class InfrastructureConfigurationSpecs
    {
        [Fact]
        public void ServicesRegistrastrationChecks()
        {
            var services = new ServiceCollection();
            services
                .AddCommonInfrastructure()
                .AddTestInfrastructure();

            var provider = services.BuildServiceProvider();
            var postRepo = provider.GetRequiredService<IDomainRepository<Post>>();
            var posts = postRepo.All();
        }
    }
}
