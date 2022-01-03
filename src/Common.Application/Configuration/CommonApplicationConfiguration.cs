namespace Portfolio.Common.Application.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class CommonApplicationConfiguration
{
    public static IServiceCollection AddCommonApplication(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<ApplicationSettings>(
                configuration.GetSection(nameof(ApplicationSettings)),
                options => options.BindNonPublicProperties = true);
}
