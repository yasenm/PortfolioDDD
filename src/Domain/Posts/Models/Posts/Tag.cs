namespace Portfolio.Domain.Posts.Models.Posts
{
    using Portfolio.Common.Domain.Models;
    using Portfolio.Domain.Posts.Exceptions;

    public class Tag : Entity<int>
    {
        internal Tag(string name)
        {
            this.Validate(name);

            this.Name = name;
        }

        public string Name { get; private set; }

        private void Validate(string name)
            => Guard.ForStringLength<InvalidTagException>(
                name,
                ModelConstants.ForTag.MinNameLength,
                ModelConstants.ForTag.MaxNameLength,
                nameof(this.Name));

    }
}
