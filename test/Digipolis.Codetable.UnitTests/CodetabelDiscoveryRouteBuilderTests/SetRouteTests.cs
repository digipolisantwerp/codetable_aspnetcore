using Digipolis.Codetable.Internal;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using Xunit;

namespace Digipolis.Codetable.UnitTests.CodetableDiscoveryRouteBuilderTests
{
    public class SetRouteTests
    {
        [Fact]
        private void NieuweRouteZelfdeAlsDefaultRouteDoetNiets()
        {
            var descriptor = new ActionDescriptor()
            {
                DisplayName = "controller",
                AttributeRouteInfo = new AttributeRouteInfo() { Template = Routes.CodetableProviderController }
            };

            var actionDescriptors = new List<ActionDescriptor>();
            actionDescriptors.Add(descriptor);

            var newRoute = Routes.CodetableProviderController;

            var builder = new CodetableDiscoveryRouteBuilder();
            builder.SetRoute(actionDescriptors, newRoute);

            Assert.Equal(Routes.CodetableProviderController, descriptor.AttributeRouteInfo.Template);
        }

        [Fact]
        private void NieuweRouteVervangtDefaultRoute()
        {
            var descriptor = new ActionDescriptor()
            {
                DisplayName = "controller",
                AttributeRouteInfo = new AttributeRouteInfo() { Template = Routes.CodetableProviderController }
            };

            var actionDescriptors = new List<ActionDescriptor>();
            actionDescriptors.Add(descriptor);

            var newRoute = "api/mijncodetables";

            var builder = new CodetableDiscoveryRouteBuilder();
            builder.SetRoute(actionDescriptors, newRoute);

            Assert.Equal("api/mijncodetables", descriptor.AttributeRouteInfo.Template);
        }
    }
}
