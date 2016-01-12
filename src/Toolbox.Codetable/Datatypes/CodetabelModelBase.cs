using System;
using System.ComponentModel.DataAnnotations;
using Toolbox.Codetable.Errors;

namespace Toolbox.Codetable.Models
{
    public class CodetabelModelBase
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = ErrorMessages.VerplichtVeld)]
        [MaxLength(50, ErrorMessage = ErrorMessages.MaxLengthVeld)]
        public string Code { get; set; }

        [Required(ErrorMessage = ErrorMessages.VerplichtVeld)]
        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthVeld)]
        public string Waarde { get; set; }

        [MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthVeld)]
        public string Omschrijving { get; set; }

        [Required(ErrorMessage = ErrorMessages.VerplichtVeld)]
        public int Volgnummer { get; set; }

        public bool Disabled { get; set; }
    }
}
