using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBiblio.Models
{
    public class Auteur
    {
        [Required(ErrorMessage = "L'identifiant de l'auteur ne peut être nul ! ")]
        public int Id { get; set; }

        [Display(Name = "Nom de l'auteur : ")] // Pour l'utiliser comme Label en HTML
        [Required(ErrorMessage = "Le nom de l'auteur ne peut être nul. (Un pseudonyme ?)"), MaxLength(50)]
        public string Nom { get; set; }
    }
}