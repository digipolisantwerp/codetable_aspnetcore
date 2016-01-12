using System;
using Toolbox.Common.Validation;

namespace Toolbox.Codetable
{
    public class CodetabelControllerInfo
    {
        public CodetabelControllerInfo(string naam, string route)
        {
            ArgumentValidator.AssertNotNullOrWhiteSpace(naam, nameof(naam));
            ArgumentValidator.AssertNotNullOrWhiteSpace(route, nameof(route));
            this.Naam = naam;
            this.Route = route;
        }

        /// <summary>
        /// De naam van de codetabel.
        /// </summary>
        public string Naam { get; private set; }

        /// <summary>
        /// De route waarop de codetabel via de api publiek is gemaakt.
        /// </summary>
        public string Route { get; private set; }
    }
}
