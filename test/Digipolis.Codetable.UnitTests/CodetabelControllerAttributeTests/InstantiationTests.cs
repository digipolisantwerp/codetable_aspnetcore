using System;
using Xunit;

namespace Digipolis.Codetable.UnitTests.CodetableControllerAttributeTests
{
    public class InstantiationTests
    {
        [Fact]
        public void DefaultNaamIsController()
        {
            var attrib = new CodetableControllerAttribute();
            Assert.Equal("[controller]", attrib.Name);
        }

        [Fact]
        public void CtorZetNaam()
        {
            const string name = "MyCodetable";
            var attrib = new CodetableControllerAttribute(name);
            Assert.Equal(name, attrib.Name);
        }

        [Fact]
        public void CtorMetNaamNullRaisesArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CodetableControllerAttribute(null));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void CtorMetNaamEmptyRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetableControllerAttribute(""));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void CtorMetNaamWhitespaceRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetableControllerAttribute("   "));
            Assert.Equal("name", ex.ParamName);
        }
    }
}
