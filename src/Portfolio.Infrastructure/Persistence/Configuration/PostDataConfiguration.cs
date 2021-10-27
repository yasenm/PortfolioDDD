namespace Portfolio.Infrastructure.Persistence.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Portfolio.Domain.Posts.Models;
    using Portfolio.Infrastructure.Persistence.Models;

    public class PostDataConfiguration : IEntityTypeConfiguration<PostData>
    {
        public void Configure(EntityTypeBuilder<PostData> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(ModelConstants.ForPost.MaxTitleLength);

            builder
                .Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(ModelConstants.ForPost.MaxContentLength);

            builder
                .HasMany(x => x.Tags)
                .WithOne(x => x.Post);
        }
    }
}
