using eBiblio.Models;
using eBiblio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBiblio.Controllers
{
    public class RechercherController : Controller
    {
        private IDal dal;

        public RechercherController() : this(new Dal())
        {
        }

        // Inversion de controle (si jamais on veut bouchonner une Dal fictive pour les tests)
        public RechercherController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(string recherche)
        {
            // La barre de recherche dans le menu amène ici
            return Index(recherche);
        }

        // GET: Rechercher
        public ActionResult Index(string id)
        {
            ResultatRecherche vm = new ResultatRecherche
            {
                recherche = id,
            };
            return View(vm);
        }

        public ActionResult Resultats(ResultatRecherche vm)
        {
            if (!string.IsNullOrWhiteSpace(vm.recherche))
            {
                List<Livre> livresTrouves = new List<Livre>(); // Tous les livres qui comporteront la chaine de caractère "id"
                List<Auteur> auteursTrouves = new List<Auteur>(); // Tous les auteurs qui comporteront la chaine de caractère "id"

                // Cherche ceux qui ont le titre qui contient vm.recherche
                foreach (Livre livre in dal.ObtientTousLesLivres())
                {
                    if (livre.Titre.IndexOf(vm.recherche, StringComparison.OrdinalIgnoreCase) >= 0)
                        livresTrouves.Add(livre);
                    if (livre.Auteur.Nom.IndexOf(vm.recherche, StringComparison.OrdinalIgnoreCase) >= 0 && !auteursTrouves.Contains(livre.Auteur))
                        auteursTrouves.Add(livre.Auteur);
                }

                vm.listeAuteursTrouves = auteursTrouves;
                vm.listeLivresTrouves = livresTrouves;

                return PartialView(vm);
            }
            else
            {
                return PartialView(vm);
            }
        }
    }
}