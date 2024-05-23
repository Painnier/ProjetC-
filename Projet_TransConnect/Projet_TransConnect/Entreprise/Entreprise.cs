using Projet_TransConnect_TANG.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Projet_TransConnect_TANG.Camion;

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
    public class Entreprise : IMoyenne
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
        public Entreprise(Salarie DirecteurG, List<Salarie> salairies, List<string[]> distances)
        {
            this.clients = new List<Client>()
            {
                new Client("1","ANG","XingTao",DateTime.Now,"95100","xingtao.tang@fff.com","0769378806","Paris"){MontantAccumule = 100},
                new Client("2","fzefNG","XingezfzfzTao",DateTime.Now,"95100","xingtao.tang@fff.com","0769204006","Marseille"){MontantAccumule = 100},
                new Client("3","TAzfzfNG","XingfzeTao",DateTime.Now,"95100","xingtao.tang@fff.com","0769312116","Monaco"){MontantAccumule = 800},
                new Client("4","zegze","evgzef",DateTime.Now,"95100","xingtao.tang@fff.com","0769376353","Toulon"){MontantAccumule = 700},
                new Client("5","jANzfzG","XinzfzgTao",DateTime.Now,"95100","xingtao.tang@fff.com","0763233886","Biarritz"){MontantAccumule = 600},
                new Client("6","dANfzefG","XizfngTao",DateTime.Now,"95100","xingtao.tang@fff.com","0760128806","Angers"){MontantAccumule = 400},
                new Client("7","cfzANG","XingfzfzTao",DateTime.Now,"95100","xingtao.tang@fff.com","0765506565","La Rochelle"){MontantAccumule = 500},
                new Client("8","wANfzfG","XinfzgTao",DateTime.Now,"95100","xingtao.tang@fff.com","0764044806","Pau"){MontantAccumule = 300},
                new Client("9","vAfzNG","XiezngTao",DateTime.Now,"95100","xingtao.tang@fff.com","0741654106","Paris"){MontantAccumule = 200}
            };
            foreach (Salarie chauffeur in salairies)
            {
                if (chauffeur.Classement == EClassement.Chauffeur)
                {
                    chauffeursdipo.Enqueue((Chauffeur)chauffeur);
                }
            }
            this.distances = distances;
            this.Organigramme = new Multitree(DirecteurG, salairies);
            this.commandesencours = new List<Commande>()
            {
                new Commande(clients[0], EnvoyerChauffeurDisponible(), new Voiture("1234", 50, 4), "Pau", distances) { Prix = 150 },
                new Commande(clients[1], EnvoyerChauffeurDisponible(), new Camionnette("789QDZAQD", 188.2, "usage"), "Marseille", distances) { Prix = 300 },
                new Commande(clients[3], EnvoyerChauffeurDisponible(), new Camion("5678", 100, TypeCamion.frigorifique), "Paris", distances) { Prix = 500 },
                new Commande(clients[4], EnvoyerChauffeurDisponible(), new Camionnette("789QDZAQD", 188.2, "usage"), "Marseille", distances) { Prix = 300 }
            };
            this.commandesfinies = new List<Commande>();
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

        #region Ajouter
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
        #endregion
        #region Chercher
        /// <summary>
        /// Recherche un salarié dans l'organigramme par son numéro de sécurité sociale.
        /// </summary>
        /// <param name="NSS">Le numéro de sécurité sociale du salarié.</param>
        /// <returns>Le salarié trouvé.</returns>
        public Salarie ChercherSalairie(string NSS)
        {
            return Multitree.ChercherSalairie(NSS, Organigramme.Head).Salairie;
        }
        /// <summary>
        /// Recherche un salarié dans l'organigramme 
        /// </summary>
        /// <param name="salairie"> Salairié.</param>
        /// <returns>Le salarié trouvé.</returns>
        public Salarie ChercherSalairie(Salarie salairie)
        {
            if (Multitree.ChercherSalairie(salairie, Organigramme.Head) != null)
            {
                return Multitree.ChercherSalairie(salairie, Organigramme.Head).Salairie;
            }
            return null;
        }
        #endregion
        #region Modification
        /// <summary>
        /// Intègre un nouveau salarié dans l'organigramme sous la supervision d'un supérieur hiérarchique.
        /// </summary>
        /// <param name="NewSalairie">Le nouveau salarié à intégrer.</param>
        /// <param name="SupSalairie">Le supérieur hiérarchique du nouveau salarié.</param>
        public void Integrer(Salarie NewSalairie, Salarie SupSalairie)
        {
            Organigramme.Integrer(NewSalairie, SupSalairie);
        }
        /// <summary>
        /// Licencie un salarié de l'entreprise.
        /// </summary>
        /// <param name="SalairieToLicencie">Le salarié à licencier.</param>
        public void Licencier(Salarie SalairieToLicencie)
        {
            Multitree.Licencier(SalairieToLicencie, Organigramme.Head);
        }
        /// <summary>
        /// Change le poste d'un salarié dans l'organigramme.
        /// </summary>
        /// <param name="NewPoste">Le nouveau poste du salarié.</param>
        /// <param name="lowsalairie">Le salarié à changer de poste.</param>
        /// <param name="supsalairie">Le supérieur hiérarchique du salarié.</param>
        public void ChangementPostSalairie(String NewPoste, Salarie lowsalairie, Salarie supsalairie)
        {
            Organigramme.ChangementPostSalairie(NewPoste, lowsalairie, supsalairie);
        }
        #endregion
        #region Affichage
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
                Console.WriteLine();
                Console.WriteLine(commande);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Affiche les commandes en cours.
        /// </summary>
        public void AfficherCommandesEncours()
        {
            foreach (Commande commande in commandesencours)
            {
                Console.WriteLine();
                Console.WriteLine(commande);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Affiche les chauffeurs disponibles.
        /// </summary>
        public void AfficherChauffeursDisponibles()
        {
            foreach (Chauffeur chauffeur in chauffeursdipo)
            {
                Console.WriteLine();
                Console.WriteLine(chauffeur);
                Console.WriteLine();
            }
        }
        #endregion
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
                    commandesencours.Remove(commande);
                }
            }
        }

        /// <summary>
        /// Retourne un chauffeur disponible.
        /// </summary>
        /// <returns>Un chauffeur disponible.</returns>
        public Chauffeur EnvoyerChauffeurDisponible()
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
        /// Le prix moyen des commandes
        /// </summary>
        /// <returns></returns>
        public double Moyenne()
        {
            double somme = 0;
            foreach (Commande commande in commandesfinies)
            {
                somme = +commande.Prix;
            }
            return somme / commandesfinies.Count;
        }
    }
}
