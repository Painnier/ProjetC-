using Projet_TransConnect_TANG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class Chauffeur : Salairie, IMoyenne
    {
        List<Commande> commandes;

        public Chauffeur(string numerosecuritesociale, string nom, string prenom, DateTime datenaissance, string adressepostale, string adresseemail, string telephone, string poste, EClassement classement, EDepartment department, int salaire, int anciennete) : base(numerosecuritesociale, nom, prenom, datenaissance, adressepostale, adresseemail, telephone, poste, classement, department, salaire, anciennete)
        {
            commandes = new List<Commande>();
        }
        public int Anciennete
        {  
            get { return anciennete; }
        }
        public override void FinCommande(Commande commande)
        {
            if (base.poste == EClassement.Chauffeur.ToString())
            {
                this.commandes.Add(commande);
            }
        }
        public string NbCommandesLivrees()
        {
            return commandes.Count.ToString();
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public double Moyenne()
        {
            double somme = 0;
            foreach (Commande commande in commandes)
            {
                somme = +commande.Prix;
            }
            return somme / commandes.Count;
        }
    }
}
