using Digipolis.Codetable.Internal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Digipolis.Codetable.UnitTests.CodetableProviderTests
{
    [CodetableController]
    public class CodetableTest1Controller
    { }

    [CodetableController("MijnCodetable")]
    public class CodetableTest2Controller
    { }

    [Route("MijnRoute")]
    [CodetableController]
    public class CodetableTest3Controller
    { }

    public class LoadTests
    {
        [Fact]
        private void ZetCodetablesProperty()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new Digipolis.Codetable.Internal.CodetableProvider(valueBuilder);

            var currentAssembly = typeof(LoadTests).GetTypeInfo().Assembly;
            provider.Load(currentAssembly);

            Assert.NotNull(provider.Codetables);
            Assert.Equal(3, provider.Codetables.Count());
        }

        [Fact]
        private void AssemblyNullRaisesArgumentNullException()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new Digipolis.Codetable.Internal.CodetableProvider(valueBuilder);

            var ex = Assert.Throws<ArgumentNullException>(() => provider.Load(null));

            Assert.Equal("assembly", ex.ParamName);
        }

        [Fact]
        private void DefaultClassNameVoorNaam()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new Digipolis.Codetable.Internal.CodetableProvider(valueBuilder);

            var currentAssembly = typeof(LoadTests).GetTypeInfo().Assembly;
            provider.Load(currentAssembly);

            Assert.Single(provider.Codetables, (ct) => ct.Name == "CodetableTest1");
        }

        [Fact]
        private void DefaultClassNameVoorRoute()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new Digipolis.Codetable.Internal.CodetableProvider(valueBuilder);

            var currentAssembly = typeof(LoadTests).GetTypeInfo().Assembly;
            provider.Load(currentAssembly);

            Assert.Single(provider.Codetables, (ct) => ct.Route == "CodetableTest1");
        }

        [Fact]
        private void CodetableControllerAttributeNaamVoorNaam()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new Digipolis.Codetable.Internal.CodetableProvider(valueBuilder);

            var currentAssembly = typeof(LoadTests).GetTypeInfo().Assembly;
            provider.Load(currentAssembly);

            Assert.Single(provider.Codetables, (ct) => ct.Route == "MijnRoute");
        }

        [Fact]
        private void RouteAttributeTemplateVoorRoute()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new Digipolis.Codetable.Internal.CodetableProvider(valueBuilder);

            var currentAssembly = typeof(LoadTests).GetTypeInfo().Assembly;
            provider.Load(currentAssembly);

            Assert.Single(provider.Codetables, (ct) => ct.Route == "MijnRoute");
        }

        [Fact]
        private void LegeCodetablesPropertyAlsGeenCodetablesInAssembly()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new Digipolis.Codetable.Internal.CodetableProvider(valueBuilder);

            var callingAssembly = Assembly.GetEntryAssembly();
            provider.Load(callingAssembly);

            Assert.NotNull(provider.Codetables);
            Assert.Equal(0, provider.Codetables.Count());
        }
    }
}
