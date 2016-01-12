using System;
using Toolbox.Codetable.Errors;
using Xunit;

namespace Toolbox.Codetable.UnitTests.CodetabelControllerInfoTests
{
    public class InstantiationTests
    {
        [Fact]
        public void CtorMetNaamNullRaisesArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CodetabelControllerInfo(null, "route"));
            Assert.Equal("naam", ex.ParamName);
        }

        [Fact]
        public void CtorMetNaamEmptyRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetabelControllerInfo("", "route"));
            Assert.Equal("naam", ex.ParamName); ;
        }

        [Fact]
        public void CtorMetNaamWhitespaceRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetabelControllerInfo("    ", "route"));
            Assert.Equal("naam", ex.ParamName);
        }

        [Fact]
        public void CtorZetNaam()
        {
            const string naam = "MijnCodetabel";
            var info = new CodetabelControllerInfo(naam, "route");
            Assert.Equal(naam, info.Naam);
        }

        [Fact]
        public void CtorMetRouteNullRaisesArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CodetabelControllerInfo("codetabel", null));
            Assert.Equal("route", ex.ParamName);
        }

        [Fact]
        public void CtorMetRouteEmptyRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetabelControllerInfo("codetabel", ""));
            Assert.Equal("route", ex.ParamName);
        }

        [Fact]
        public void CtorMetRouteWhitespaceRaisesArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => new CodetabelControllerInfo("codetabel", "    "));
            Assert.Equal("route", ex.ParamName);
        }

        [Fact]
        public void CtorZetRoute()
        {
            var info = new CodetabelControllerInfo("MijnCodetabel", "MijnRoute");
            Assert.Equal("MijnRoute", info.Route);
        }
    }
}
