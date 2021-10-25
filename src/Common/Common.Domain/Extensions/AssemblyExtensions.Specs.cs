namespace Portfolio.Common.Domain.Extensions
{
    using FluentAssertions;
    using System.Linq;
    using System.Reflection;
    using Xunit;

    public class AssemblyExtensionsSpecs
    {
        [Fact]
        public void TestGettingGenericTypeInterfaces()
        {
            var interfaces = Assembly.GetExecutingAssembly().GetTypesByGenericInterfaceType(typeof(ITestGenericInterface<>));

            interfaces.Should().HaveCount(1);
            interfaces.First().Key.Should().Be(typeof(ITestGenericInterface<object>));
            interfaces.First().Value.Should().Be(typeof(GenericImplementationOfITestGenericInterface));
        }

        private interface ITestGenericInterface<T> { }
        private class GenericImplementationOfITestGenericInterface : ITestGenericInterface<object> { }
    }
}
