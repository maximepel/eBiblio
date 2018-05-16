using eBiblio.Models;
using eBiblio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eBiblio.Controllers
{
    [Authorize] // Il faut etre authentifié
    public class CreerController : Controller
    {
        private IDal dal;

        public CreerController() : this(new Dal())
        {
        }

        // Inversion de controle (si jamais on veut bouchonner une Dal fictive pour les tests)
        public CreerController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        // Pour utiliser l'echantillon de test, il faut entrer dans l'URL /Afficher/CreerEchantillon 
        // Une seule fois car ça enregistre en BDD EntityFramework, sinon vous faites des doublons et des erreurs de doublons
        public ActionResult Echantillon()
        {
            dal.CreerEchantillon();
            return View();
        }

        // GET: Creer
        [AllowAnonymous]
        public ActionResult Livre()
        {
            CreerLivre vm = new CreerLivre
            {
                Auteurs = new SelectList(dal.ObtientTousLesAuteurs(), "Id", "Nom"),
            };
            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Livre(CreerLivre vm)
        {
            vm.Auteurs = new SelectList(dal.ObtientTousLesAuteurs(), "Id", "Nom");
            // Verifier livre existe pas deja (titre) et non vide
            if (string.IsNullOrWhiteSpace(vm.Titre))
            {
                ModelState.AddModelError("Titre", "ERR_SERVER : Le titre est vide.");
                return View(vm);
            }
            else
            {
                if (dal.LivreExiste(vm.Titre))
                {
                    ModelState.AddModelError("Titre", "ERR_SERVER : Ce titre de livre existe déjà.");
                    return View(vm);
                }
                else
                {
                    if (!ModelState.IsValid)
                        return View(vm);

                    // Succès
                    Auteur auteur = dal.ChercheAuteur(vm.AuteurId);
                    dal.CreerLivre(vm.Titre, vm.DateDeParution.Date, auteur);
                    return RedirectToAction("Index", "Afficher");
                }
            }
        }

        // GET: Creer
        public ActionResult Auteur()
        {
            Auteur auteur = new Auteur();
            return View(auteur);
        }

        [HttpPost]
        public ActionResult Auteur(Auteur auteur)
        {
            // Verifier auteur existe pas deja (nom) et non vide
            if (string.IsNullOrWhiteSpace(auteur.Nom))
            {
                ModelState.AddModelError("Nom", "ERR_SERVER : Entrez un nom, débile!");
                return View(auteur);
            }
            else
            {
                if (dal.AuteurExiste(auteur.Nom))
                {
                    ModelState.AddModelError("Nom", "ERR_SERVER : Cet auteur existe déjà.");
                    return View(auteur);
                }
                else
                {
                    if (!ModelState.IsValid)
                        return View(auteur);

                    dal.CreerAuteur(auteur.Nom);
                    return RedirectToAction("Auteurs", "Afficher");
                }
            }
        }

    }
}