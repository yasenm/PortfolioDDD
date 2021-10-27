namespace Portfolio.Domain.Posts.Models.Posts
{
    using Portfolio.Common.Domain.Models;
    using Portfolio.Domain.Posts.Events;
    using Portfolio.Domain.Posts.Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    public class Post : Entity<int>
    {
        private readonly HashSet<Tag> tags;

        public Post(string title, string content)
        {
            this.Validate(title, content);

            this.Title = title;
            this.Content = content;

            this.tags = new HashSet<Tag>();
        }

        public string Title { get; private set; }
        public string Content { get; private set; }
        public IReadOnlyCollection<Tag> Tags => this.tags.ToList().AsReadOnly();

        public Post UpdateTitle(string title)
        {
            this.ValidateTitle(title);
            this.Title = title;

            return this;
        }

        public Post UpdateContent(string content)
        {
            this.ValidateContent(content);
            this.Content = content;

            return this;
        }

        public void AddTag(string name)
        {
            this.tags.Add(new Tag(name));

            this.RaiseEvent(new TagAddedEvent());
        }

        private void Validate(string title, string content)
        {
            this.ValidateTitle(title);
            this.ValidateContent(content);
        }

        private void ValidateTitle(string title)
            => Guard.ForStringLength<InvalidPostException>(
                title,
                ModelConstants.ForPost.MinTitleLength,
                ModelConstants.ForPost.MaxTitleLength,
                nameof(this.Content));

        private void ValidateContent(string content)
            => Guard.ForStringLength<InvalidPostException>(
                content,
                ModelConstants.ForPost.MinContentLength,
                ModelConstants.ForPost.MaxContentLength,
                nameof(this.Content));

    }
}