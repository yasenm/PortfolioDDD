namespace Portfolio.Infrastructure.Persistence.Models
{
    using Portfolio.Common.Application.Mapping;
    using Portfolio.Domain.Posts.Models.Posts;

    public class TagData : IMapFrom<Tag>, IMapTo<Tag>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PostId { get; set; }
        public virtual PostData Post { get; set; }
    }
}
