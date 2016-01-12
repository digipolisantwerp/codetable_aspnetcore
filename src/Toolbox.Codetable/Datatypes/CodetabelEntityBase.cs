using System;
using System.ComponentModel.DataAnnotations;
using Toolbox.DataAccess.Entities;

namespace Toolbox.Codetable.Entities
{
    /// <summary>
    /// Base class voor codetabel entiteiten.
    /// </summary>
    public class CodetabelEntityBase : EntityBase
    {
        /// <summary>
        /// De code voor waarde in de codetabel (verplicht, max lengte = 50).
        /// </summary>
        [Required]
        [MaxLength(50)]      
        public string Code { get; set; }

        /// <summary>
        /// De waarde in de codetabel (verplicht, max lengte = 250).
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Waarde { get; set; }

        /// <summary>
        /// Een optionele extra omschrijving voor de waarde in de codetabel.
        /// </summary>
        [MaxLength(250)]
        public string Omschrijving { get; set; }

        /// <summary>
        /// Een volgnummer voor de waarde in de codetabel. Deze kan gebruikt worden om te sorteren (verplicht).
        /// </summary>
        [Required]
        public int Volgnummer { get; set; }

        /// <summary>
        /// Een optie om de waarde in de codetabel te disablen zodat ze bvb niet meer kan gekozen worden door de gebruiker (verplicht).
        /// </summary>
        [Required]
        public bool Disabled { get; set; }
    }
}
