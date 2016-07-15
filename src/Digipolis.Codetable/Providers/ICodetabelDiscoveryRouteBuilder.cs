using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Collections.Generic;

namespace Digipolis.Codetable
{
    public interface ICodetableDiscoveryRouteBuilder
    {
        void SetRoute(IEnumerable<ActionDescriptor> actionDescriptors, string route);
    }
}
