using Moq;
using System;
using System.Linq;
using Xunit;

namespace Digipolis.Codetable.UnitTests.CodetableProviderTests
{
    public class InstantiationTests
    {
        [Fact]
        public void CodetablesPropertyIsDefaultLegeLijst()
        {
            var valueBuilder = Mock.Of<IValueBuilder>();
            var provider = new Digipolis.Codetable.Internal.CodetableProvider(valueBuilder);
            Assert.NotNull(provider.Codetables);
            Assert.Equal(0, provider.Codetables.Count());
        }

        [Fact]
        public void ValueBuilderNullRaisesArgumentNullException()
        {
            IValueBuilder nullValueBuilder = null;
            var ex = Assert.Throws<ArgumentNullException>(() => new Digipolis.Codetable.Internal.CodetableProvider(nullValueBuilder));
            Assert.Equal("valueBuilder", ex.ParamName);
        }
    }
}
