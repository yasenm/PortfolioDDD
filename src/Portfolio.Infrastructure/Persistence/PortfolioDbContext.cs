namespace Portfolio.Infrastructure.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Portfolio.Infrastructure.Persistence.Models;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    internal class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<PostData> Posts { get; set; } = null;  
        public DbSet<TagData> Tags { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
