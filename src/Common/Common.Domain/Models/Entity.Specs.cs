namespace Portfolio.Common.Domain.Models
{
    using FluentAssertions;
    using Xunit;
    using System.Reflection;
    using System;
    using System.Linq;

    public class EntitySpecs
    {
        [Fact]
        public void WhenComparedToOtherEntity()
        {
            var first = new TestEntity();
            var second = new TestEntity();

            first.Equals(second).Should().BeTrue();
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 2, false)]
        [InlineData(int.MinValue, int.MaxValue, false)]
        public void CompareByIdWithExpectedResult(int firstEntityId, int secondEntityId, bool expectedToBeEqual)
        {
            var first = new TestEntity().SetId(firstEntityId);
            var second = new TestEntity().SetId(secondEntityId);

            first.Equals(second).Should().Be(expectedToBeEqual);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 2, false)]
        [InlineData(int.MinValue, int.MaxValue, false)]
        [InlineData(1, 1, false, false)]
        [InlineData(1, 2, true, false)]
        [InlineData(int.MinValue, int.MaxValue, true, false)]
        public void CompareWithOperatorsExpectedResult(int firstEntityId, int secondEntityId, bool expectedToBeEqual, bool isEqualsOperator = true)
        {
            var first = new TestEntity().SetId(firstEntityId);
            var second = new TestEntity().SetId(secondEntityId);

            var equalsResult = isEqualsOperator switch
            {
                true => first == second,
                false => first != second
            };

            equalsResult.Should().Be(expectedToBeEqual);
        }

        private class TestEntity : Entity<int> { }
    }

    internal static class EntityExtensions
    {
        internal static Entity<TId> SetId<TId>(this Entity<TId> entity, TId id)
            where TId : struct
        {
            entity
                 .GetType()
                 .BaseType!
                 .GetProperty(nameof(entity.Id))!
                 .GetSetMethod(true)!
                 .Invoke(entity, new object[] { id });

            return entity;
        }
    }
}