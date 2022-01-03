namespace Portfolio.Common.Infrastructure.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class CommonInfrastructureConfiguration
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
}