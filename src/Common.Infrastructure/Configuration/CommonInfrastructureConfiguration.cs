namespace Common.Infrastructure.Configuration
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class CommonInfrastructureConfiguration
    {
        public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
