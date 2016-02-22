using System;
using System.Reflection;
using Xunit;

namespace Toolbox.Codetable.UnitTests.CodetableDiscoveryOptionsTests
{
    public class Startup
    { }

    public class InstantiationTests
    {
        [Fact]
        private void DefaultRouteIsSet()
        {
            var options = new CodetableDiscoveryOptions();
            Assert.Equal(Routes.CodetableProviderController, options.Route);
        }

        [Fact]
        private void DefaultControllerAssemblyIsSet()
        {
            var options = new CodetableDiscoveryOptions();
            Assert.NotNull(options.ControllerAssembly);
        }

        [Fact]
        private void DefaultInstantiatesObjectWithDefaults()
        {
            var options = CodetableDiscoveryOptions.Default;
            Assert.NotNull(options);
            Assert.Equal(Routes.CodetableProviderController, options.Route);
            Assert.NotNull(options.ControllerAssembly);
        }
    }
}
