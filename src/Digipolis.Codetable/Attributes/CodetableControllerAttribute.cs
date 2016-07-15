using Digipolis.Common.Validation;
using System;

namespace Digipolis.Codetable
{
    /// <summary>
    /// Indicates that a controller makes a code table public in the api..
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class CodetableControllerAttribute : Attribute
    {
        internal const string DefaultName = "[controller]";

        public CodetableControllerAttribute()
        {
            this.Name = DefaultName;
        }

        public CodetableControllerAttribute(string name)
        {
            ArgumentValidator.AssertNotNullOrWhiteSpace(name, nameof(name));
            this.Name = name;
        }

        /// <summary>
        /// Name under which the codetable should appear in the list. If this is not filled in, the name of the controlles is taken as name.
        /// </summary>
        public string Name { get; private set; }
    }
}
