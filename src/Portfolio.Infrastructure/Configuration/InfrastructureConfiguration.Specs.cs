namespace Portfolio.Infrastructure.Configuration
{
    using Common.Infrastructure.Configuration;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Portfolio.Application.Contracts;
    using Portfolio.Domain.Posts.Models.Posts;
    using Xunit;

    public class InfrastructureConfigurationSpecs
    {
        [Fact]
        public void ServicesRegistrastrationChecks()
        {
            var services = new ServiceCollection();
            services
                .AddCommonInfrastructure(default)
                .AddTestInfrastructure();

            var provider = services.BuildServiceProvider();
            var postRepo = provider.GetRequiredService<IDomainRepository<Post>>();
            var posts = postRepo.All();

            posts.Should().HaveCount(0);
        }
    }
}
