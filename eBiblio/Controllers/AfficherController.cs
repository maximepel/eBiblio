using eBiblio.Models;
using eBiblio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBiblio.Controllers
{
    public class AfficherController : Controller
    {
        private IDal dal;

        public AfficherController() : this(new Dal())
        {
        }

        // Inversion de controle (si jamais on veut bouchonner une Dal fictive pour les tests)
        public AfficherController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        // GET: / ou /Afficher ou /Afficher/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET partiel dans /
        public ActionResult ListeLivres()
        {
            // Liste de tous les livres (titre + auteur)
            List<Livre> listeLivres = dal.ObtientTousLesLivres();
            return PartialView(listeLivres.OrderBy(r => r.Titre).ToList());
        }

        public ActionResult Auteurs()
        {
            return View();
        }

        // GET partiel dans /Afficher/Auteurs
        public ActionResult ListeAuteurs()
        {
            // Liste de tous les auteurs
            List<Auteur> listeAuteurs = dal.ObtientTousLesAuteurs();
            return PartialView(listeAuteurs.OrderBy(r => r.Nom).ToList());
        }

        public ActionResult Auteur(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (Int32.TryParse(id, out int i) && i > 0 && dal.ChercheAuteur(i) != null)
                {
                    LivresPourUnAuteur vm = new LivresPourUnAuteur
                    {
                        Auteur = dal.ChercheAuteur(i),                  // Obtenir l'auteur
                        ListeLivres = dal.ObtientLivresDUnAuteur(i)     // Obtenir tous les livres de cet auteur
                    };
                    return View(vm);
                }
                else return View("ErrorAuthor");
            }
            else return View("ErrorAuthor");  
        }

        public ActionResult Livre(string id)
        {
            // Affiche un livre (titre + date de parution + emprunteur si existant)
            if (!string.IsNullOrWhiteSpace(id))
            {
                if (Int32.TryParse(id, out int i) && i > 0 && dal.ChercheLivre(i) != null)
                {
                    Livre livre = dal.ChercheLivre(i);
                    return View(livre);
                }
                else return View("ErrorBook");
            }
            else return View("ErrorBook");
        }
    }
}