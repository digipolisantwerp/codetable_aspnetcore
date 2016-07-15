using System;
using Xunit;

namespace Digipolis.Codetable.UnitTests.CodetableControllerInfoTests
{
    public class InstantiationTests
    {
        [Fact]
        public void CtorMetNaamNullRaisesArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CodetableControllerInfo(null, "route"));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void CtorMetNaamEmptyRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetableControllerInfo("", "route"));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void CtorMetNaamWhitespaceRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetableControllerInfo("    ", "route"));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void CtorZetNaam()
        {
            const string name = "MyCodetable";
            var info = new CodetableControllerInfo(name, "route");
            Assert.Equal(name, info.Name);
        }

        [Fact]
        public void CtorMetRouteNullRaisesArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CodetableControllerInfo("codetable", null));
            Assert.Equal("route", ex.ParamName);
        }

        [Fact]
        public void CtorMetRouteEmptyRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetableControllerInfo("codetable", ""));
            Assert.Equal("route", ex.ParamName);
        }

        [Fact]
        public void CtorMetRouteWhitespaceRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetableControllerInfo("codetable", "    "));
            Assert.Equal("route", ex.ParamName);
        }

        [Fact]
        public void CtorZetRoute()
        {
            var info = new CodetableControllerInfo("MijnCodetable", "MijnRoute");
            Assert.Equal("MijnRoute", info.Route);
        }
    }
}
