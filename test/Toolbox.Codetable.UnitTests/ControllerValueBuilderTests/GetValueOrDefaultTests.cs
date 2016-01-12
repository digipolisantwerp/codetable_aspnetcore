using Toolbox.Codetable.Internal;
using Xunit;

namespace Toolbox.Codetable.UnitTests.ControllerValueBuilderTests
{
    public class GetValueOrDefaultTests
    {
        [Fact]
        private void DefaultZonderControllerGeeftDefault()
        {
            var builder = new ControllerValueBuilder();
            var result = builder.GetValueOrDefault("aValueController", "aDefaultValue");
            Assert.Equal("aDefaultValue", result);
        }

        [Fact]
        private void DefaultIsControllerGeeftValue()
        {
            var builder = new ControllerValueBuilder();
            var result = builder.GetValueOrDefault("aValueController", "[controller]");
            Assert.Equal("aValue", result);
        }

        [Fact]
        private void DefaultIsControllerHoofdletterGeeftValue()
        {
            var builder = new ControllerValueBuilder();
            var result = builder.GetValueOrDefault("aValueController", "[Controller]");
            Assert.Equal("aValue", result);
        }

        [Fact]
        private void DefaultBevatControllerGeeftValueMetDefaultPart()
        {
            var builder = new ControllerValueBuilder();
            var result = builder.GetValueOrDefault("aValueController", "api/[controller]");
            Assert.Equal("api/aValue", result);
        }

        [Fact]
        private void DefaultBevatControllerHoofdletterGeeftValueMetDefaultPart()
        {
            var builder = new ControllerValueBuilder();
            var result = builder.GetValueOrDefault("aValueController", "api/[Controller]");
            Assert.Equal("api/aValue", result);
        }

        [Fact]
        private void DefaultBevatControllerRandomHoofdletterGeeftValueMetDefaultPart()
        {
            var builder = new ControllerValueBuilder();
            var result = builder.GetValueOrDefault("aValueController", "api/[ContRolLEr]");
            Assert.Equal("api/aValue", result);
        }

    }
}
