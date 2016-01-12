using System;
using Xunit;

namespace Toolbox.Codetable.UnitTests.CodetabelControllerAttributeTests
{
    public class InstantiationTests
    {
        [Fact]
        public void DefaultNaamIsController()
        {
            var attrib = new CodetabelControllerAttribute();
            Assert.Equal("[controller]", attrib.Naam);
        }

        [Fact]
        public void CtorZetNaam()
        {
            const string naam = "MijnCodetabel";
            var attrib = new CodetabelControllerAttribute(naam);
            Assert.Equal(naam, attrib.Naam);
        }

        [Fact]
        public void CtorMetNaamNullRaisesArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CodetabelControllerAttribute(null));
            Assert.Equal("naam", ex.ParamName);
        }

        [Fact]
        public void CtorMetNaamEmptyRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetabelControllerAttribute(""));
            Assert.Equal("naam", ex.ParamName);
        }

        [Fact]
        public void CtorMetNaamWhitespaceRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetabelControllerAttribute("   "));
            Assert.Equal("naam", ex.ParamName);
        }
    }
}
