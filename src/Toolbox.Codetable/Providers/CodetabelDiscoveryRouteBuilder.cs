using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Mvc.Routing;

namespace Toolbox.Codetable.Internal
{
    public class CodetabelDiscoveryRouteBuilder : ICodetabelDiscoveryRouteBuilder
    {
        public void SetRoute(IEnumerable<ActionDescriptor> actionDescriptors, string route)
        {
            foreach ( var controller in actionDescriptors )
            {
                var oldTemplate = controller.AttributeRouteInfo.Template;
                var newTemplate = oldTemplate.Replace(Routes.CodetabelProviderController, route);
                controller.AttributeRouteInfo = new AttributeRouteInfo() { Template = newTemplate };
            }
        }
    }
}
