using System;
using Toolbox.Common.Validation;

namespace Toolbox.Codetable
{
    /// <summary>
    /// Geeft aan dat een controller een codetabel publiek maakt op de api.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CodetabelControllerAttribute : Attribute
    {
        internal const string DefaultNaam = "[controller]";

        public CodetabelControllerAttribute()
        {
            this.Naam = DefaultNaam;
        }

        public CodetabelControllerAttribute(string naam)
        {
            ArgumentValidator.AssertNotNullOrWhiteSpace(naam, nameof(naam));
            this.Naam = naam;
        }

        /// <summary>
        /// Naam waaronder de codetabel in de lijst moet komen. Als deze niet ingevuld wordt, wordt de naam de controller (zonder Controller) genomen.
        /// </summary>
        public string Naam { get; private set; }
    }
}
