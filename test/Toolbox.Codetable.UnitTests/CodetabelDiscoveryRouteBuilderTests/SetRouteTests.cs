using System;
using System.Collections.Generic;
using Toolbox.Codetable.Internal;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Routing;
using Xunit;
using Microsoft.AspNet.Mvc.Abstractions;

namespace Toolbox.Codetable.UnitTests.CodetabelDiscoveryRouteBuilderTests
{
    public class SetRouteTests
    {
        [Fact]
        private void NieuweRouteZelfdeAlsDefaultRouteDoetNiets()
        {
            var descriptor = new ActionDescriptor()
            {
                DisplayName = "controller",
                AttributeRouteInfo = new AttributeRouteInfo() { Template = Routes.CodetabelProviderController }
            };

            var actionDescriptors = new List<ActionDescriptor>();
            actionDescriptors.Add(descriptor);

            var newRoute = Routes.CodetabelProviderController;

            var builder = new CodetabelDiscoveryRouteBuilder();
            builder.SetRoute(actionDescriptors, newRoute);

            Assert.Equal(Routes.CodetabelProviderController, descriptor.AttributeRouteInfo.Template);
        }

        [Fact]
        private void NieuweRouteVervangtDefaultRoute()
        {
            var descriptor = new ActionDescriptor()
            {
                DisplayName = "controller",
                AttributeRouteInfo = new AttributeRouteInfo() { Template = Routes.CodetabelProviderController }
            };

            var actionDescriptors = new List<ActionDescriptor>();
            actionDescriptors.Add(descriptor);

            var newRoute = "api/mijncodetabellen";

            var builder = new CodetabelDiscoveryRouteBuilder();
            builder.SetRoute(actionDescriptors, newRoute);

            Assert.Equal("api/mijncodetabellen", descriptor.AttributeRouteInfo.Template);
        }
    }
}
