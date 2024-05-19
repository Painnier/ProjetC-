using Projet_TransConnect_TANG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public enum EClassement
    {
        Directeur,
        ChefEquipe,
        Salairie,
        Chauffeur,
        Formation,
        Contrats,
        Comptable,
        Commercial,
        Stagier
    }
    public enum EDepartment
    {
        Commerciale,
        Operation,
        RH,
        Financier
    }
    public class Entreprise
    {
        List<Commande> commandes;
        List<Client> clients;
        public Entreprise()
        {
            this.commandes = new List<Commande>();
            this.clients = new List<Client>();
        }
        public void AjouterClient(Client client)
        {
            this.clients.Add(client);
        }
        public void AjouterCommande(Commande commande)
        {
            this.commandes.Add(commande);
        }
        private void CommandesByDates(DateTime DateA, DateTime DateB) 
        {
            foreach (Commande commande in this.commandes)
            {
                if(commande.CreateDate > DateA && commande.CreateDate > DateB)
                {
                    Console.WriteLine(commande.ToString());
                }
            }
        }
    }
}
