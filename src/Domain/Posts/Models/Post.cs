namespace Portfolio.Domain.Posts.Models
{
    using Common.Domain.Models;

    public class Post : Entity<int>
    {
        internal Post(string title, string content)
        {
            this.Title = title;
            this.Content = content;
        }

        public string Title { get; private set; }
        public string Content { get; private set; }
    }
}