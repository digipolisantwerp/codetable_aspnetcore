using System;
using System.Linq;
using Toolbox.Codetable.Internal;
using Moq;
using Xunit;

namespace Toolbox.Codetable.UnitTests.CodetableProviderTests
{
    public class InstantiationTests
    {
        [Fact]
        public void CodetablesPropertyIsDefaultLegeLijst()
        {
            var valueBuilder = Mock.Of<IValueBuilder>();
            var provider = new Internal.CodetableProvider(valueBuilder);
            Assert.NotNull(provider.Codetables);
            Assert.Equal(0, provider.Codetables.Count());
        }

        [Fact]
        public void ValueBuilderNullRaisesArgumentNullException()
        {
            IValueBuilder nullValueBuilder = null;
            var ex = Assert.Throws<ArgumentNullException>(() => new Internal.CodetableProvider(nullValueBuilder));
            Assert.Equal("valueBuilder", ex.ParamName);
        }
    }
}
