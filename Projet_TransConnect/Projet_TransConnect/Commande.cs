using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class Commande
    {
        Client client;
        string livraisonAtoB;
        double prix;
        Salarie chauffeur;
        //Vehicule 
        DateTime date;

        public Commande(Client client, double prix, Salarie chauffeur, DateTime date)
        {
            this.client = client;
            this.prix = prix;
            this.chauffeur = chauffeur;
            this.date = date;
        }  

        public double Prix
        {
            get { return prix; }
        }

        public override string ToString()
        {
            return client.ToString() + livraisonAtoB;
        }
    }
}
    