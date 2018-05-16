using eBiblio.Models;
using eBiblio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eBiblio.Controllers
{
    public class LoginController : Controller
    {
        // Le lien avec la base de données et les méthodes de la Lib d'accès DAL
        private IDal dal;

        // Par défaut
        public LoginController() : this(new Dal())
        {

        }

        // Dans le cas où on précise une autre DAL (cas d'une DAL de tests sans base de données SQL)
        private LoginController(IDal dalIoc)
        {
            dal = dalIoc;
        }


        public ActionResult Index()
        {
            // HttpContext.User.Identity.IsAuthenticated récupère l'information dans le cookie
            UtilisateurViewModel viewModel = new UtilisateurViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                viewModel.Utilisateur = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(UtilisateurViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid) // vérifie que le formulaire de Log-In est correct
            {
                Utilisateur utilisateur = dal.Authentifier(viewModel.Utilisateur.Pseudo, viewModel.Utilisateur.MotDePasse);
                if (utilisateur != null)
                {
                    // Si c'est correct, on crée un cookie à partir de Id de Utilisateur ; false signifie durée limitée dans le temps
                    FormsAuthentication.SetAuthCookie(utilisateur.Pseudo, false); // Cette méthode renseigne le "HttpContext.User.Identity"

                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)) // Url.IsLocalUrl Vérifie qu'on ne renvoie pas les infos vers un site externe 
                        return Redirect(returnUrl); //Redirection à l'endroit qui avait été demandé avant d'être redirigé vers la page d'authentifcation
                    return Redirect("/");
                }
                ModelState.AddModelError("Utilisateur.Pseudo", "Pseudo et/ou mot de passe incorrect(s)");
            }
            return View(viewModel);
        }

        public ActionResult CreerCompte()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerCompte(CreerUtilisateurViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Utilisateur.MotDePasse == vm.ConfirmationMotDePasse) // On vérifie les deux champs
                {
                    int id = dal.AjouterUtilisateur(vm.Utilisateur.Pseudo, vm.Utilisateur.MotDePasse);
                    FormsAuthentication.SetAuthCookie(vm.Utilisateur.Pseudo, false);
                    return Redirect("/");
                }
                ModelState.AddModelError("ConfirmationMotDePasse", "ERR : La confirmation n'est pas identique au mot de passe.");
            }
            return View(vm);
        }

        public ActionResult Deconnexion()
        {
            FormsAuthentication.SignOut(); //Supprime le cookie d'authenfication
            return Redirect("/");
        }
    }
}