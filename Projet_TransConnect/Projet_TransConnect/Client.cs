using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class Client : Personne
    {
        List <Commande> commandes_client;
        double mantant_accumule=0;
        public Client(int numerosecuritesociale, string nom, string prenom, DateTime datenaissance, string adressepostale, string adresseemail, int telephone)
            : base(numerosecuritesociale, nom, prenom, datenaissance, adressepostale, adresseemail, telephone) { }

        public void FinCommande(Commande commande)
        {
            this.commandes_client.Append(commande);
            this.mantant_accumule = this.mantant_accumule + commande.Prix;
        }
        public List<Commande> Commandes_client
        {
            get { return commandes_client;}
        }
        public double Mantant_accumule
        {
            get { return mantant_accumule; }
        }
    }
}
