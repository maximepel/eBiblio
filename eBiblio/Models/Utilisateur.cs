using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eBiblio.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }

        [Required, MaxLength(80)] // DataAnnotation pour la BDD SQL de taille de chaine de caractère
        [Display(Name = "Pseudo : ")]
        public string Pseudo { get; set; }

        [Required]
        [Display(Name = "Mot de passe : ")]
        public string MotDePasse { get; set; }

        // Exemple : lambda/default ; premium ; administrator ; moderator
        public string Statut { get; set; }
    }
}