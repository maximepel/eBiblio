using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBiblio.Models
{
    public interface IDal : IDisposable
    {
        List<Livre> ObtientTousLesLivres();
        List<Auteur> ObtientTousLesAuteurs();
        List<Livre> ObtientLivresDUnAuteur(int id);
    
        Livre ChercheLivre(int id);
        bool LivreExiste(string titre);

        Auteur ChercheAuteur(string nom);
        Auteur ChercheAuteur(int id);
        bool AuteurExiste(string titre);

        Client ChercheClient(string email);
        
        void CreerLivre(string titre, DateTime dateParution, Auteur auteur);
        void CreerAuteur(string nom);
        void CreerClient(string nom, string email);

        void EmprunterLivre(Client client, Livre livre);
        void RendreLivre(Client client, Livre livre);

        void CreerEchantillon();
    }
}
