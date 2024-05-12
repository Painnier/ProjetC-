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
    public class Client : Personne, IComparable
    {
        List<Commande> commandesclient;
        double montantaccumule = 0;
        Criteres critere;

        String ville;
        public Client(int numerosecuritesociale, string nom, string prenom, DateTime datenaissance, string adressepostale, string adresseemail, int telephone, string ville)
            : base(numerosecuritesociale, nom, prenom, datenaissance, adressepostale, adresseemail, telephone)
        {
            this.ville = ville;
        }

        public void FinCommande(Commande commande)
        {
            this.commandesclient.Append(commande);
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
        public int CompareTo(object obj)
        {
            Client c = obj as Client;

            switch (this.critere)
            {
                case Criteres.ville:
                    return this.ville.CompareTo(c.ville);

                case Criteres.nom:
                    return this.nom.CompareTo(c.nom);

                case Criteres.montantaccumule:
                    return this.montantaccumule.CompareTo(c.montantaccumule);

                default:
                    return this.nom.CompareTo(c.nom);
            }

        }
    }
}
