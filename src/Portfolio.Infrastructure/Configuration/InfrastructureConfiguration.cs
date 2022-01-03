namespace Portfolio.Infrastructure.Configuration
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Portfolio.Application.Contracts;
    using Portfolio.Common.Application;
    using Portfolio.Domain.Posts.Models.Posts;
    using Portfolio.Infrastructure.Identity;
    using Portfolio.Infrastructure.Persistence;
    using Portfolio.Infrastructure.Persistence.Models;
    using Portfolio.Infrastructure.Persistence.Repositories;
    using System.Reflection;
    using System.Text;

    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddMapping()
                .AddDbContextSetup(configuration)
                .AddIdentity(configuration)
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

        private static IServiceCollection AddIdentity(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<PortfolioDbContext>();

            var secret = configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));

            var key = Encoding.ASCII.GetBytes(secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddTransient<IIdentity, IdentityService>();

            return services;
        }
    }
}
