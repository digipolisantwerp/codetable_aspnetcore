using System;

namespace Toolbox.Codetable
{
    public class CodetabelInfo
    {
        /// <summary>
        /// De naam van de codetabel.
        /// </summary>
        public string Naam { get; set; }

        /// <summary>
        /// De route waar de codetabel via de api kan aangesproken worden.
        /// </summary>
        public string Route { get; set; }
    }
}
