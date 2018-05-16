using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace eBiblio.Models
{
    public class Dal : IDal
    {
        private BddContext bdd;
        private static bool premiereInstance = true;

        public Dal()
        {
            bdd = new BddContext();
            // Charge la BDD EntityFramework 
            // => onglet "Explorateur de serveurs"
            // => "Connexion à la base de données"
            //      => Source de données : Microsoft SQL Server
            //      => Nom du serveur : "(localdb)\MSSQLLocalDB"
            //      => Nom base de données : "eBiblio.Models.BddContext"
        }

        public List<Livre> ObtientLivresDUnAuteur(int id)
        {
            return bdd.Livres.Where(l => l.Auteur.Id == id).ToList();
        }

        public List<Auteur> ObtientTousLesAuteurs()
        {
            return bdd.Auteurs.ToList();
        }

        public List<Livre> ObtientTousLesLivres()
        {
            return bdd.Livres.ToList();
        }

        public Livre ChercheLivre(int id)
        {
            return bdd.Livres.FirstOrDefault(l => l.Id == id);
        }

        public bool LivreExiste(string titre)
        {
            return bdd.Livres.Any(livre => string.Compare(livre.Titre, titre, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        public Auteur ChercheAuteur(string nom)
        {
            return bdd.Auteurs.FirstOrDefault(a => a.Nom == nom);
        }

        public Auteur ChercheAuteur(int id)
        {
            return bdd.Auteurs.FirstOrDefault(a => a.Id == id);
        }

        public bool AuteurExiste(string nom)
        {
            return bdd.Auteurs.Any(auteur => string.Compare(auteur.Nom, nom, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        public Client ChercheClient(string email)
        {
            return bdd.Clients.FirstOrDefault(a => a.Email == email);
        }

        public void CreerLivre(string titre, DateTime dateParution, Auteur auteur)
        {
            bdd.Livres.Add(new Livre { Titre = titre, DateDeParution = dateParution.Date, Auteur = auteur });
            bdd.SaveChanges();
        }

        public void CreerAuteur(string nom)
        {
            bdd.Auteurs.Add(new Auteur { Nom = nom });
            bdd.SaveChanges();
        }

        public void CreerClient(string nom, string email)
        {
            bdd.Clients.Add(new Client { Email = email, Nom = nom, Livres = new List<Livre>() });
            bdd.SaveChanges();
        }

        public void EmprunterLivre(Client client, Livre livre)
        {
            // On modifie le client
            // On modifie le livre
            if (client != null && livre != null)
            {
                client.Livres.Add(livre);
                livre.Client = client;

                bdd.Livres.First(l => l.Id == livre.Id).Client = client;
                bdd.SaveChanges();
            }
        }

        public void RendreLivre(Client client, Livre livre)
        {
            // On modifie le client
            // On modifie le livre
            if (client != null && livre != null)
            {
                client.Livres.Remove(livre);
                livre.Client = null;
                bdd.SaveChanges();
            }
        }


        /*
        ###################################################################################
        #######################              AUTH             #############################
        ###################################################################################
        */
        public int AjouterUtilisateur(string pseudo, string motDePasse)
        {
            string motDePasseEncode = EncodeMD5(motDePasse);
            Utilisateur utilisateur = new Utilisateur { Pseudo = pseudo, MotDePasse = motDePasseEncode, Statut = "Lambda" };
            bdd.Utilisateurs.Add(utilisateur);
            bdd.SaveChanges();
            return utilisateur.Id;
        }

        public Utilisateur Authentifier(string pseudo, string motDePasse)
        {
            string motDePasseEncode = EncodeMD5(motDePasse);
            return bdd.Utilisateurs.FirstOrDefault(u => u.Pseudo == pseudo && u.MotDePasse == motDePasseEncode);
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return bdd.Utilisateurs.FirstOrDefault(u => u.Id == id);
        }

        public Utilisateur ObtenirUtilisateur(string idString)
        {
            int id;
            if (int.TryParse(idString, out id))
                return ObtenirUtilisateur(id);
            return null;
        }

        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "debut encodage" + motDePasse + "fin encodage";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
        /*
        ###################################################################################
        ###################################################################################
        */

        public void Dispose()
        {
            bdd.Dispose();
        }

        public void CreerEchantillon()
        {
            if (premiereInstance)
            {
                // Auteurs
                CreerAuteur("Charles BAUDELAIRE");
                CreerAuteur("Jean GIONO");
                CreerAuteur("Joseph JOFFO");
                CreerAuteur("Alphonse DANLETAS");

                // Livres
                CreerLivre("Les fleurs du mal", new DateTime(1857, 6, 25).Date, ChercheAuteur("Charles BAUDELAIRE"));
                CreerLivre("Le Hussard sur le toit", new DateTime(1951, 4, 25).Date, ChercheAuteur("Jean GIONO"));
                CreerLivre("Un sac de billes", new DateTime(1973, 1, 15).Date, ChercheAuteur("Joseph JOFFO"));
                CreerLivre("Le Partage", new DateTime(2005, 6, 15).Date, ChercheAuteur("Joseph JOFFO"));
                CreerLivre("Le Bonheur fou", new DateTime(1957, 8, 12).Date, ChercheAuteur("Jean GIONO"));
                CreerLivre("La mascarade", new DateTime(2008, 2, 12).Date, ChercheAuteur("Alphonse DANLETAS"));

                // Clients
                CreerClient("Nicolas DURAND", "nicolas.durand@oc.fr");
                CreerClient("Stéphanie DELPECH", "stefany_delpech@freete.com");
                CreerClient("Alphonse TOUDROIT", "jaimelhumour@lol.net");

                EmprunterLivre(ChercheClient("nicolas.durand@oc.fr"), ChercheLivre(1));
                EmprunterLivre(ChercheClient("nicolas.durand@oc.fr"), ChercheLivre(6));
                EmprunterLivre(ChercheClient("stefany_delpech@freete.com"), ChercheLivre(2));
                EmprunterLivre(ChercheClient("stefany_delpech@freete.com"), ChercheLivre(3));
                EmprunterLivre(ChercheClient("stefany_delpech@freete.com"), ChercheLivre(4));

                premiereInstance = false;
            }
        }
    }
}