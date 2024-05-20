using Projet_TransConnect_TANG.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    enum Criteres
    {
        ville,
        nom,
        montantaccumule
    }
    public class Client : Personne, IComparable<Client>, IMoyenne
    {
        List<Commande> commandesclient;
        double montantaccumule;
        Criteres critere= Criteres.nom;

        String ville;
        public Client(int numerosecuritesociale, string nom, string prenom, DateTime datenaissance, string adressepostale, string adresseemail, string telephone, string ville)
            : base(numerosecuritesociale, nom, prenom, datenaissance, adressepostale, adresseemail, telephone)
        {
            this.commandesclient = new List<Commande>();
            this.ville = ville;
            this.montantaccumule = 0;
        }
        public void FinCommande(Commande commande)
        {
            this.commandesclient.Add(commande);
        }
        public List<Commande> CommandesClient
        {
            get { return commandesclient; }
        }
        public double MontantAccumule
        {
            get { return montantaccumule; }
        }
        public void Accumuler()
        {
            foreach (Commande commande in commandesclient)
            {
                this.montantaccumule = this.montantaccumule + commande.Prix;
            }
        }
        public void ChangerCritere(String critere) 
        { 
            switch (critere)
            {
                case "ville" :
                    this.critere = Criteres.ville;
                    break;
                case "nom":
                    this.critere = Criteres.nom;
                    break;
                case "montantaccumule":
                    this.critere = Criteres.montantaccumule;
                    break;
                default:
                    this.critere = Criteres.nom;
                    break;
            }
        }
        public int CompareTo(Client client)
        {
            switch (this.critere)
            {
                case Criteres.ville:
                    return this.ville.CompareTo(client.ville);

                case Criteres.nom:
                    return this.nom.CompareTo(client.nom);

                case Criteres.montantaccumule:
                    return this.montantaccumule.CompareTo(client.montantaccumule);

                default:
                    return this.nom.CompareTo(client.nom);
            }
        }
        public void AfficherCommandes()
        {
            foreach(Commande commande in commandesclient)
            {
                Console.WriteLine(commande.ToString());
            }
        }
        public double Moyenne()
        {
            double somme = 0;
            foreach(Commande commande in commandesclient)
            {
                somme = +commande.Prix;
            }
            return somme/commandesclient.Count;
        }
    }
}
