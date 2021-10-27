namespace Portfolio.Infrastructure.Persistence.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Portfolio.Domain.Posts.Models;
    using Portfolio.Infrastructure.Persistence.Models;

    public class TagDataConfiguration : IEntityTypeConfiguration<TagData>
    {
        public void Configure(EntityTypeBuilder<TagData> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(ModelConstants.ForTag.MaxNameLength);

            builder
                .HasOne(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.PostId);
        }
    }
}
