using System;
using Toolbox.Common.Validation;

namespace Toolbox.Codetable
{
    public class CodetableControllerInfo
    {
        public CodetableControllerInfo(string name, string route)
        {
            ArgumentValidator.AssertNotNullOrWhiteSpace(name, nameof(name));
            ArgumentValidator.AssertNotNullOrWhiteSpace(route, nameof(route));
            this.Name = name;
            this.Route = route;
        }

        /// <summary>
        /// The name of the codetable.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The route where codetable is exposed via the api.
        /// </summary>
        public string Route { get; private set; }
    }
}
