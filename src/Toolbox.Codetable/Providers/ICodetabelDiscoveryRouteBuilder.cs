using System;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Abstractions;

namespace Toolbox.Codetable
{
    public interface ICodetabelDiscoveryRouteBuilder
    {
        void SetRoute(IEnumerable<ActionDescriptor> actionDescriptors, string route);
    }
}
