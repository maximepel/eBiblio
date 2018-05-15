using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eBiblio.Models
{
    public class Livre
    {
        [Required(ErrorMessage = "ERR_CLIENT : Identifiant du Livre requis.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "ERR_CLIENT : Titre du livre requis.")]
        [Display(Name = "Titre du livre : ")]
        public string Titre { get; set; }

        [Required(ErrorMessage = "ERR_CLIENT : Date de parution/publication requise.")]
        [Display(Name = "Date de parution : ")]
        public DateTime DateDeParution { get; set; }

        public virtual Auteur Auteur { get; set; }
        public virtual Client Client { get; set; }
    }
}