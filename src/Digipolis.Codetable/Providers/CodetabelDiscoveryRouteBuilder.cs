using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;

namespace Digipolis.Codetable.Internal
{
    public class CodetableDiscoveryRouteBuilder : ICodetableDiscoveryRouteBuilder
    {
        public void SetRoute(IEnumerable<ActionDescriptor> actionDescriptors, string route)
        {
            foreach ( var controller in actionDescriptors )
            {
                var oldTemplate = controller.AttributeRouteInfo.Template;
                var newTemplate = oldTemplate.Replace(Routes.CodetableProviderController, route);
                controller.AttributeRouteInfo = new AttributeRouteInfo() { Template = newTemplate };
            }
        }
    }
}
