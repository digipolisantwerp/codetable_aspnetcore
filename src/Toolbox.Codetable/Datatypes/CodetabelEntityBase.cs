using System;
using System.ComponentModel.DataAnnotations;
using Toolbox.DataAccess.Entities;

namespace Toolbox.Codetable.Entities
{
    /// <summary>
    /// Base class for codetable entities
    /// </summary>
    public class CodetableEntityBase : EntityBase
    {
        /// <summary>
        /// The code for values in the codetable (required, max length = 50).
        /// </summary>
        [Required]
        [MaxLength(50)]      
        public string Code { get; set; }

        /// <summary>
        /// De waarde in de codetable (required, max length = 250).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Value { get; set; }

        /// <summary>
        /// An optional extra description for the value in the codetable.
        /// </summary>
        [MaxLength(250)]
        public string Description { get; set; }

        /// <summary>
        /// A Serial number for the value in the code table.  This can be used to sort (required).
        /// </summary>
        [Required]
        public int Sortindex { get; set; }

        /// <summary>
        /// An option to disable the value in the codetable, so it cannot be chosen by users anymore for example (required).
        /// </summary>
        [Required]
        public bool Disabled { get; set; }
    }
}
