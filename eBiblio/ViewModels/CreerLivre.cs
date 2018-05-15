using eBiblio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBiblio.ViewModels
{
    public class CreerLivre
    {
        public SelectList Auteurs { get; set; }
        public int AuteurId { get; set; }

        [Required(ErrorMessage = "ERR_CLIENT : Titre du livre requis.")]
        [Display(Name = "Titre du livre : ")]
        public String Titre { get; set; }

        [Required(ErrorMessage = "ERR_CLIENT : Date requise, au bon format (jj/MM/aaaa).")]
        [Display(Name = "Date de parution : ")]
        public DateTime DateDeParution { get; set; }
    }
}