namespace Portfolio.Domain.Posts.Models.Posts
{
    using FluentAssertions;
    using Portfolio.Domain.Posts.Exceptions;
    using System;
    using Xunit;

    public class PostSpecs
    {
        [Theory]
        [InlineData(3, 52, false)]
        [InlineData(1, 52, true)]
        [InlineData(51, 52, true)]
        [InlineData(3, 40, true)]
        [InlineData(3, 100_000, true)]
        [InlineData(1, 100_000, true)]
        [InlineData(51, 100_000, true)]
        [InlineData(51, 40, true)]
        public void CreatePost_CheckConstructorValidations(int titleLength, int contentLength, bool expecetedToThrowException)
        {
            Func<Post> act = () => new Post(new string('*', titleLength), new string('*', contentLength));

            if (expecetedToThrowException)
            {
                act.Should().Throw<InvalidPostException>();
            }
            else
            {
                act.Should().NotThrow<InvalidPostException>();
            }
        }
    }
}
