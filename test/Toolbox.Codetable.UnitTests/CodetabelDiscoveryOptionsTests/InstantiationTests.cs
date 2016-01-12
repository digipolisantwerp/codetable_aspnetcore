using System;
using System.Reflection;
using Xunit;

namespace Toolbox.Codetable.UnitTests.CodetabelDiscoveryOptionsTests
{
    public class Startup
    { }

    public class InstantiationTests
    {
        [Fact]
        private void DefaultRouteIsSet()
        {
            var options = new CodetabelDiscoveryOptions();
            Assert.Equal(Routes.CodetabelProviderController, options.Route);
        }

        [Fact]
        private void DefaultControllerAssemblyIsSet()
        {
            var options = new CodetabelDiscoveryOptions();
            Assert.NotNull(options.ControllerAssembly);
        }

        [Fact]
        private void DefaultInstantiatesObjectWithDefaults()
        {
            var callingAssembly = Assembly.GetCallingAssembly();
            var options = CodetabelDiscoveryOptions.Default;
            Assert.NotNull(options);
            Assert.Equal(Routes.CodetabelProviderController, options.Route);
            Assert.NotNull(options.ControllerAssembly);
        }

        [Fact]
        private void ControllerAssemblyIsSet()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            var options = new CodetabelDiscoveryOptions(executingAssembly);
            Assert.Same(executingAssembly, options.ControllerAssembly);
        }
    }
}
