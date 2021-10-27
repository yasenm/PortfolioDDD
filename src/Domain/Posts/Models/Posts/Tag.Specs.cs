namespace Portfolio.Domain.Posts.Models.Posts
{
    using FluentAssertions;
    using Portfolio.Domain.Posts.Exceptions;
    using System;
    using Xunit;

    public class TagSpecs
    {
        [Theory]
        [InlineData(1, false)]
        [InlineData(500, false)]
        [InlineData(10, true)]
        public void ConstructorTests(int nameLength, bool isExpectedToBeValid)
        {
            var generatedName = new string('*', nameLength);
            Func<Tag> ctor = () => new Tag(generatedName);

            if (isExpectedToBeValid)
            {
                ctor.Should().NotThrow<InvalidTagException>();

                var tag = ctor.Invoke();
                tag.Should().NotBeNull();
                tag.Name.Should().Be(generatedName);
            }
            else
            {
                ctor.Should().Throw<InvalidTagException>();
            }
        }
    }
}
