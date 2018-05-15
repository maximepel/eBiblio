﻿using eBiblio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBiblio.ViewModels
{
    public class ResultatRecherche
    {
        public String recherche { get; set; }
        public int nombreLivresTrouves { get; set; }
        public List<Livre> listeLivresTrouves { get; set; }
        public int nombreAuteursTrouves { get; set; }
        public List<Auteur> listeAuteursTrouves { get; set; }
    }
}