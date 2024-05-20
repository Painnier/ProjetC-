using Projet_TransConnect_TANG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        List<Salairie> salairies;
        Multitree Organigramme;
        public Entreprise(Salairie DirecteurG, List<Salairie> salairies, List<string[]> Distances)
        {
            String[] Starts = Distances[0];
            String[] Ends = Distances[1];
            int[] Kms = ConvertHelper.ToIntArray(Distances[2]);
            int[] Time = ConvertHelper.HoursToMins(Distances[3]);
            String[] vexss = ConvertHelper.Villes(Starts, Ends);
            EData[] edgess = new EData[Starts.Length];
            this.commandes = new List<Commande>();
            this.clients = new List<Client>();
            this.salairies = salairies;
            this.Organigramme = new Multitree(DirecteurG, salairies);
        }
        public void AjouterClient(Client client)
        {
            this.clients.Add(client);
        }
        public void AjouterCommande(Commande commande)
        {
            this.commandes.Add(commande);
        }
        public Salairie ChercherSalairie(int NSS)
        {
            return Multitree.ChercherSalairie(NSS, Organigramme.Head).Salairie;
        }
        public bool Integrer(Salairie NewSalairie, Salairie SupSalairie)
        {
            return Organigramme.Integrer(NewSalairie, SupSalairie);
        }
        public void Licencier(Salairie SalairieToLicencie)
        {
            Multitree.Licencier(SalairieToLicencie, Organigramme.Head);
        }
        public void ChangementPostSalairie(String NewPoste, Salairie lowsalairie, Salairie supsalairie)
        {
            Organigramme.ChangementPostSalairie(NewPoste, lowsalairie, supsalairie);
        }
        public void AfficherOrganigramme()
        {
            Organigramme.AfficherOrganigramme();
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
