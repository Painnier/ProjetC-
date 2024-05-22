using Projet_TransConnect_TANG.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_TransConnect_TANG
{
    /// <summary>
    /// Enumération représentant les différents classements des salariés.
    /// </summary>
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

    /// <summary>
    /// Enumération représentant les différents départements de l'entreprise.
    /// </summary>
    public enum EDepartment
    {
        Commerciale,
        Operation,
        RH,
        Financier
    }

    /// <summary>
    /// Classe représentant une entreprise avec ses salariés, clients, commandes et organigramme.
    /// </summary>
    public class Entreprise
    {
        List<Commande> commandesfinies; // Liste des commandes terminées
        List<Commande> commandesencours; // Liste des commandes en cours
        List<Client> clients; // Liste des clients
        Multitree Organigramme; // Organigramme de l'entreprise
        Queue<Chauffeur> chauffeursdipo = new Queue<Chauffeur>(); // File de chauffeurs disponibles
        List<string[]> distances; // Liste des distances

        /// <summary>
        /// Constructeur de la classe Entreprise.
        /// </summary>
        /// <param name="DirecteurG">Le directeur général de l'entreprise.</param>
        /// <param name="salairies">Liste des salariés de l'entreprise.</param>
        /// <param name="distances">Liste des distances.</param>
        public Entreprise(Salairie DirecteurG, List<Salairie> salairies, List<string[]> distances)
        {
            this.commandesfinies = new List<Commande>();
            this.commandesencours = new List<Commande>();
            this.clients = new List<Client>();
            this.Organigramme = new Multitree(DirecteurG, salairies);
            this.distances = distances;
            foreach (Salairie chauffeur in salairies)
            {
                if (chauffeur.Classement == EClassement.Chauffeur)
                {
                    chauffeursdipo.Enqueue((Chauffeur)chauffeur);
                }
            }
        }

        #region Property

        /// <summary>
        /// Propriété pour obtenir la liste des distances.
        /// </summary>
        public List<string[]> Distances
        {
            get { return distances; }
        }

        /// <summary>
        /// Propriété pour obtenir la liste des clients.
        /// </summary>
        public List<Client> Clients
        {
            get { return clients; }
        }

        /// <summary>
        /// Propriété pour obtenir la liste des commandes terminées.
        /// </summary>
        public List<Commande> CommandesFinies
        {
            get { return commandesfinies; }
        }

        /// <summary>
        /// Propriété pour obtenir la liste des commandes en cours.
        /// </summary>
        public List<Commande> CommandesEnCours
        {
            get { return commandesencours; }
        }

        /// <summary>
        /// Propriété pour obtenir la file des chauffeurs disponibles.
        /// </summary>
        public Queue<Chauffeur> ChauffeursDiponible
        {
            get { return chauffeursdipo; }
        }

        #endregion

        /// <summary>
        /// Ajoute un client à la liste des clients de l'entreprise.
        /// </summary>
        /// <param name="client">Le client à ajouter.</param>
        public void AjouterClient(Client client)
        {
            this.clients.Add(client);
        }

        /// <summary>
        /// Ajoute une commande à la liste des commandes en cours.
        /// </summary>
        /// <param name="commande">La commande à ajouter.</param>
        public void AjouterCommandeEnCours(Commande commande)
        {
            this.commandesencours.Add(commande);
        }

        /// <summary>
        /// Ajoute une commande à la liste des commandes terminées.
        /// </summary>
        /// <param name="commande">La commande à ajouter.</param>
        public void AjouterCommandeFinie(Commande commande)
        {
            this.commandesfinies.Add(commande);
        }

        /// <summary>
        /// Recherche un salarié dans l'organigramme par son numéro de sécurité sociale.
        /// </summary>
        /// <param name="NSS">Le numéro de sécurité sociale du salarié.</param>
        /// <returns>Le salarié trouvé.</returns>
        public Salairie ChercherSalairie(string NSS)
        {
            return Multitree.ChercherSalairie(NSS, Organigramme.Head).Salairie;
        }

        /// <summary>
        /// Intègre un nouveau salarié dans l'organigramme sous la supervision d'un supérieur hiérarchique.
        /// </summary>
        /// <param name="NewSalairie">Le nouveau salarié à intégrer.</param>
        /// <param name="SupSalairie">Le supérieur hiérarchique du nouveau salarié.</param>
        public void Integrer(Salairie NewSalairie, Salairie SupSalairie)
        {
            Organigramme.Integrer(NewSalairie, SupSalairie);
        }

        /// <summary>
        /// Licencie un salarié de l'entreprise.
        /// </summary>
        /// <param name="SalairieToLicencie">Le salarié à licencier.</param>
        public void Licencier(Salairie SalairieToLicencie)
        {
            Multitree.Licencier(SalairieToLicencie, Organigramme.Head);
        }

        /// <summary>
        /// Change le poste d'un salarié dans l'organigramme.
        /// </summary>
        /// <param name="NewPoste">Le nouveau poste du salarié.</param>
        /// <param name="lowsalairie">Le salarié à changer de poste.</param>
        /// <param name="supsalairie">Le supérieur hiérarchique du salarié.</param>
        public void ChangementPostSalairie(String NewPoste, Salairie lowsalairie, Salairie supsalairie)
        {
            Organigramme.ChangementPostSalairie(NewPoste, lowsalairie, supsalairie);
        }

        /// <summary>
        /// Affiche l'organigramme de l'entreprise.
        /// </summary>
        public void AfficherOrganigramme()
        {
            Organigramme.AfficherOrganigramme();
        }

        /// <summary>
        /// Affiche les commandes terminées.
        /// </summary>
        public void AfficherCommandesFinies()
        {
            foreach (Commande commande in commandesfinies)
            {
                Console.WriteLine(commande);
            }
        }

        /// <summary>
        /// Affiche les commandes en cours.
        /// </summary>
        public void AfficherCommandesEncours()
        {
            foreach (Commande commande in commandesencours)
            {
                Console.WriteLine(commande);
            }
        }

        /// <summary>
        /// Affiche les chauffeurs disponibles.
        /// </summary>
        public void AfficherChauffeursDisponibles()
        {
            foreach (Chauffeur chauffeur in chauffeursdipo)
            {
                Console.WriteLine(chauffeur);
            }
        }

        /// <summary>
        /// Termine une commande en cours.
        /// </summary>
        /// <param name="index">L'index de la commande à terminer.</param>
        public void FinCommande(int index)
        {
            Commande commande = SearchCommandeEnCours(index);
            foreach (Client client in clients)
            {
                if (client != null && client.NumeroSecuriteSociale == commande.Client.NumeroSecuriteSociale)
                {
                    commande.Effectuee();
                    client.FinCommande(commande);
                    ChercherSalairie(commande.Chauffeur.NumeroSecuriteSociale).FinCommande(commande);
                    this.commandesfinies.Add(commande);
                    chauffeursdipo.Enqueue((Chauffeur)ChercherSalairie(commande.Chauffeur.NumeroSecuriteSociale));
                }
            }
        }

        /// <summary>
        /// Retourne un chauffeur disponible.
        /// </summary>
        /// <returns>Un chauffeur disponible.</returns>
        public Chauffeur ChauffeurDisponible()
        {
            return chauffeursdipo.Dequeue();
        }

        /// <summary>
        /// Recherche une commande en cours par son index.
        /// </summary>
        /// <param name="index">L'index de la commande.</param>
        /// <returns>La commande trouvée.</returns>
        private Commande SearchCommandeEnCours(int index)
        {
            return this.commandesencours[index];
        }

        /// <summary>
        /// Affiche les commandes terminées entre deux dates.
        /// </summary>
        /// <param name="DateA">La date de début.</param>
        /// <param name="DateB">La date de fin.</param>
        private void CommandesByDates(DateTime DateA, DateTime DateB)
        {
            foreach (Commande commande in this.commandesfinies)
            {
                if (commande.CreateDate > DateA && commande.CreateDate > DateB)
                {
                    Console.WriteLine(commande.ToString());
                }
            }
        }
    }
}
