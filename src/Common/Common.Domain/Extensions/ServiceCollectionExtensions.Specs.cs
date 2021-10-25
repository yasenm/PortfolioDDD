namespace Portfolio.Common.Domain.Extensions
{
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Reflection;
    using Xunit;

    public class ServiceCollectionExtensionsSpecs
    {
        [Fact]
        public void VerifyServiceExtensionAddFactoriesScansAssemblyAndAddsFactories()
        {
            var serviceCollection = new ServiceCollection();

            var provider = serviceCollection.AddDomain(Assembly.GetExecutingAssembly()).BuildServiceProvider();
            var scope = provider.CreateScope();
            var factories = scope.ServiceProvider.GetServices(typeof(IFactory<TestRoot>)).ToList();

            factories.Count.Should().Be(1);
            factories[0]!.GetType().Should().Be(typeof(TestFactory));
        }

        private class TestFactory : IFactory<TestRoot> { }

        private class TestRoot : IAggregateRoot { }
    }
}
