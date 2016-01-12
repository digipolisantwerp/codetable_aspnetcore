using System;
using System.Linq;
using Toolbox.Codetable.Internal;
using Moq;
using Xunit;

namespace Toolbox.Codetable.UnitTests.CodetabelProviderTests
{
    public class InstantiationTests
    {
        [Fact]
        public void CodetabellenPropertyIsDefaultLegeLijst()
        {
            var valueBuilder = Mock.Of<IValueBuilder>();
            var provider = new CodetabelProvider(valueBuilder);
            Assert.NotNull(provider.Codetabellen);
            Assert.Equal(0, provider.Codetabellen.Count());
        }

        [Fact]
        public void ValueBuilderNullRaisesArgumentNullException()
        {
            IValueBuilder nullValueBuilder = null;
            var ex = Assert.Throws<ArgumentNullException>(() => new CodetabelProvider(nullValueBuilder));
            Assert.Equal("valueBuilder", ex.ParamName);
        }
    }
}
