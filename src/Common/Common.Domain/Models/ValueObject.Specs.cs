namespace Portfolio.Common.Domain.Models
{
    using FluentAssertions;
    using Xunit;

    public class ValueObjectSpecs
    {
        [Theory]
        [InlineData(1, true, "test", 1, true, "test", true)]
        [InlineData(1, false, "test", 1, false, "test", true)]
        [InlineData(int.MaxValue, false, "test", int.MaxValue, false, "test", true)]
        [InlineData(1, false, "test2", 1, false, "test2", true)]
        [InlineData(1, true, "test", 1, true, "test2", false)]
        [InlineData(1, true, "test", 2, true, "test", false)]
        [InlineData(1, true, "test", 1, false, "test", false)]
        [InlineData(2, true, "test", 1, true, "test", false)]
        [InlineData(1, false, "test", 1, true, "test", false)]
        [InlineData(1, true, "test1", 1, true, "test", false)]
        [InlineData(2, true, "test", 1, true, "test2", false)]
        public void ValueObjectsEqualsTests(int obj1prop1, bool obj1prop2, string obj1prop3, int obj2prop1, bool obj2prop2, string obj2prop3, bool expectedToBeEqual)
        {
            var firstValObj = new TestValueObject(obj1prop1, obj1prop2, obj1prop3);
            var secondValObj = new TestValueObject(obj2prop1, obj2prop2, obj2prop3);

            (firstValObj == secondValObj).Should().Be(expectedToBeEqual);
            (firstValObj.GetHashCode() == secondValObj.GetHashCode()).Should().Be(expectedToBeEqual);
        }

        private class TestValueObject : ValueObject
        {
            internal TestValueObject(int prop1, bool prop2, string prop3)
            {
                this.Prop1 = prop1;
                this.Prop2 = prop2;
                this.Prop3 = prop3;
            }

            public int Prop1 { get; private set; }
            public bool Prop2 { get; private set; }
            public string Prop3 { get; private set; }
        }
    }
}
