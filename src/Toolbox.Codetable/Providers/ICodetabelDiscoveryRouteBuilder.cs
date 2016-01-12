using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Abstractions;

namespace Toolbox.Codetable
{
    public interface ICodetableDiscoveryRouteBuilder
    {
        void SetRoute(IEnumerable<ActionDescriptor> actionDescriptors, string route);
    }
}
