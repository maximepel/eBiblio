using eBiblio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBiblio.ViewModels
{
    public class LivresPourUnAuteur
    {
        public Auteur Auteur { get; set; }
        public List<Livre> ListeLivres { get; set; }
    }
}