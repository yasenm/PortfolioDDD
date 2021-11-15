namespace Portfolio.Common.Domain.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using System.Reflection;

    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddGenericTransientServices<T>(this IServiceCollection services, Assembly assembly, ServiceScopeType serviceScopeType = ServiceScopeType.Transient)
        {
            var genType = typeof(T);
            var types = assembly.GetTypesByGenericInterfaceType(genType);

            foreach (var type in types)
            {
                if (serviceScopeType == ServiceScopeType.Transient)
                {
                    services.TryAddTransient(type.Key, type.Value);
                }
                else if (serviceScopeType == ServiceScopeType.Scoped)
                {
                    services.TryAddScoped(type.Key, type.Value);
                }
                else
                {
                    services.TryAddSingleton(type.Key, type.Value);
                }
            }

            return services;
        }
    }
}
