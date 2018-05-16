using eBiblio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eBiblio.ViewModels
{
    public class CreerUtilisateurViewModel
    {
        public Utilisateur Utilisateur { get; set; }

        [Required]
        [Display(Name = "Confirmez le mot de passe : ")]
        public string ConfirmationMotDePasse { get; set; }
    }
}