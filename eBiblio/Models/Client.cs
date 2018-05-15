using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eBiblio.Models
{
    public class Client
    {
        [Key, Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le nom du client ne peut être nul. (Un pseudonyme ?)"), MaxLength(50)]
        public string Nom { get; set; }
        public virtual List<Livre> Livres { get; set; }

    }
}