namespace Portfolio.Infrastructure.Persistence.Models
{
    using Portfolio.Common.Application.Mapping;
    using Portfolio.Domain.Posts.Models.Posts;
    using System.Collections.Generic;

    public class PostData : IMapFrom<Post>, IMapTo<Post>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<TagData> Tags { get; set; }
    }
}
