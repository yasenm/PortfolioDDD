namespace Portfolio.Common.Domain.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System.Linq;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services, Assembly assembly)
            => services
                .AddFactories(assembly);

        private static IServiceCollection AddFactories(this IServiceCollection services, Assembly assembly)
        {
            var factoryType = typeof(IFactory<>);
            var types = assembly.GetTypesByGenericInterfaceType(factoryType);
            foreach (var type in types)
            {
                services.TryAddTransient(type.Key, type.Value);
            }

            return services;
        }
    }
}
