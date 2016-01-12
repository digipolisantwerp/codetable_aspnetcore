using System;
using System.Linq;
using System.Reflection;
using Toolbox.Codetable.Errors;
using Toolbox.Codetable.Internal;
using Microsoft.AspNet.Mvc;
using Xunit;

namespace Toolbox.Codetable.UnitTests.CodetabelProviderTests
{
    [CodetabelController]
    public class CodetabelTest1Controller
    { }

    [CodetabelController("MijnCodetabel")]
    public class CodetabelTest2Controller
    { }

    [Route("MijnRoute")]
    [CodetabelController]
    public class CodetabelTest3Controller
    { }

    public class LoadTests
    {
        [Fact]
        private void ZetCodetabellenProperty()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new CodetabelProvider(valueBuilder);

            var currentAssembly = Assembly.GetExecutingAssembly();
            provider.Load(currentAssembly);

            Assert.NotNull(provider.Codetabellen);
            Assert.Equal(3, provider.Codetabellen.Count());
        }

        [Fact]
        private void AssemblyNullRaisesArgumentNullException()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new CodetabelProvider(valueBuilder);

            var ex = Assert.Throws<ArgumentNullException>(() => provider.Load(null));

            Assert.Equal("assembly", ex.ParamName);
        }

        [Fact]
        private void DefaultClassNameVoorNaam()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new CodetabelProvider(valueBuilder);

            var currentAssembly = Assembly.GetExecutingAssembly();
            provider.Load(currentAssembly);

            Assert.Single(provider.Codetabellen, (ct) => ct.Naam == "CodetabelTest1");
        }

        [Fact]
        private void DefaultClassNameVoorRoute()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new CodetabelProvider(valueBuilder);

            var currentAssembly = Assembly.GetExecutingAssembly();
            provider.Load(currentAssembly);

            Assert.Single(provider.Codetabellen, (ct) => ct.Route == "CodetabelTest1");
        }

        [Fact]
        private void CodetabelControllerAttributeNaamVoorNaam()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new CodetabelProvider(valueBuilder);

            var currentAssembly = Assembly.GetExecutingAssembly();
            provider.Load(currentAssembly);

            Assert.Single(provider.Codetabellen, (ct) => ct.Route == "MijnRoute");
        }

        [Fact]
        private void RouteAttributeTemplateVoorRoute()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new CodetabelProvider(valueBuilder);

            var currentAssembly = Assembly.GetExecutingAssembly();
            provider.Load(currentAssembly);

            Assert.Single(provider.Codetabellen, (ct) => ct.Route == "MijnRoute");
        }

        [Fact]
        private void LegeCodetabellenPropertyAlsGeenCodetabellenInAssembly()
        {
            var valueBuilder = new ControllerValueBuilder();
            var provider = new CodetabelProvider(valueBuilder);

            var callingAssembly = Assembly.GetCallingAssembly();
            provider.Load(callingAssembly);

            Assert.NotNull(provider.Codetabellen);
            Assert.Equal(0, provider.Codetabellen.Count());
        }
    }
}
