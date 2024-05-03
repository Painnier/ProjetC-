using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    delegate int Algorithme(string A, String B);
    public class Commande
    {
        Client client;
        string livraisonAtoB;
        double prix;
        Salarie chauffeur;
        DateTime date;
        Vehicule vehicule;
        string A, B;

        public Commande(Client client, double prix, Salarie chauffeur, DateTime date, Vehicule vehicule, string A, String B)
        {
            this.client = client;
            this.prix = prix;
            this.chauffeur = chauffeur;
            this.date = date;
            this.vehicule = vehicule;
            this.A = A;
            this.B = B;
        }
        public double Prix
        {
            get { return prix; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        public Client Client 
        { 
            get { return client; } 
        }
        public Salarie Chauffeur
        {
            get { return chauffeur; }
            set { chauffeur = value; }
        }
        public Vehicule Vehicule
        {
            get { return vehicule; }
            set { vehicule = value; }
        }
        public override string ToString()
        {
            return "Client : " + client.ToString() + ", prix : " + prix.ToString() + ", chauffeur : [" + chauffeur.ToString() + "], date : " + date.ToString() + ", véhicule : " + vehicule + ", trafic : " +  livraisonAtoB;
        }
        //public double CalculeurPrix(string A, string B, Algorithme Dijkstra)
        //{

        //}
        public int Dijkstra(string A, string B)
        {
            return 0;
        }
    }
}
    