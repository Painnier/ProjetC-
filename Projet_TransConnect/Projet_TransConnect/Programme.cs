using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Projet_TransConnect_TANG.Camion;

namespace Projet_TransConnect_TANG
{
    class Programme
    {
        static Entreprise TransConnect;

        [STAThread]
        public static void Main(string[] args)
        {
            InitialiserEntreprise();

            while (true)
            {
                AfficherMenuPrincipal();
                var choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        GererClients();
                        break;
                    case "2":
                        GererSalaries();
                        break;
                    case "3":
                        GererCommandes();
                        break;
                    case "4":
                        AfficherStatistiques();
                        break;
                    case "5":
                        GererTestsUnitaires();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
        }

        /// <summary>
        /// Initialise les données de l'entreprise, notamment les salariés et les distances.
        /// </summary>
        static void InitialiserEntreprise()
        {
            // Lecture de fichier Distance.csv et initialisation des données
            List<string[]> Distances = CSVHelper.ReadCSV(@"..\..\Distances.csv");

            // Directeur Général
            Salairie Dupond = new Salairie("123456789", "Dupond", "Jean", new DateTime(1965, 4, 23), "Adresse Dupond", "dupond@company.com", "1234567890", "Directeur Général", EClassement.Directeur, EDepartment.Operation, 8000, 20);

            List<Salairie> salairies = new List<Salairie>
            {
                // Direction Commerciale
                new Salairie("234567890", "Fiesta", "Marie", new DateTime(1970, 6, 15), "Adresse Fiesta", "fiesta@company.com", "2345678901", "Directrice Commerciale", EClassement.Directeur, EDepartment.Commerciale, 6000,1),
                new Salairie("345678901", "Forge", "Pierre", new DateTime(1975, 8, 10), "Adresse Forge", "forge@company.com", "3456789012", "Commercial", EClassement.Commercial, EDepartment.Commerciale, 4000,2),
                new Salairie("456789012", "Fermi", "Sophie", new DateTime(1980, 9, 5), "Adresse Fermi", "fermi@company.com", "4567890123", "Commerciale", EClassement.Commercial, EDepartment.Commerciale, 4000,3),

                // Direction des Opérations
                new Salairie("567890123", "Fetard", "Louis", new DateTime(1968, 11, 20), "Adresse Fetard", "fetard@company.com", "5678901234", "Directeur des opérations", EClassement.Directeur, EDepartment.Operation, 7000,4),
                new Salairie("678901234", "Royal", "Claude", new DateTime(1972, 1, 30), "Adresse Royal", "royal@company.com", "6789012345", "Chef Equipe", EClassement.ChefEquipe, EDepartment.Operation, 5000,4),
                new Chauffeur("789012345", "Romu", "Jacques", new DateTime(1985, 3, 25), "Adresse Romu", "romu@company.com", "7890123456", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 5),
                new Chauffeur("890123456", "Romi", "Pascal", new DateTime(1988, 4, 12), "Adresse Romi", "romi@company.com", "7890123456", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 4),
                new Chauffeur("901234567", "Roma", "Thierry", new DateTime(1990, 5, 18), "Adresse Roma", "roma@company.com", "9012345678", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 3),

                new Salairie("112345678", "Prince", "Claire", new DateTime(1973, 2, 22), "Adresse Prince", "prince@company.com", "1123456789", "Chef d'Equipe", EClassement.ChefEquipe, EDepartment.Operation, 5000,5),
                new Chauffeur("223456789", "Rome", "Isabelle", new DateTime(1986, 7, 15), "Adresse Rome", "rome@company.com", "2234567890", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 6),
                new Chauffeur("334567890", "Rimou", "Aline", new DateTime(1989, 8, 21), "Adresse Rimou", "rimou@company.com", "3345678901", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 4),

                // Direction des Ressources Humaines
                new Salairie("445678901", "Joyeuse", "Lucie", new DateTime(1978, 10, 2), "Adresse Joyeuse", "joyeuse@company.com", "4456789012", "Directrice des RH", EClassement.Directeur, EDepartment.RH, 6000,6),
                new Salairie("556789012", "Couleur", "Nathalie", new DateTime(1982, 11, 9), "Adresse Couleur", "couleur@company.com", "5567890123", "Formation", EClassement.Formation, EDepartment.RH, 4000,5),
                new Salairie("667890123", "TouteleMonde", "Caroline", new DateTime(1985, 12, 12), "Adresse TouteleMonde", "toutelemonde@company.com", "6678901234", "Contrats", EClassement.Contrats, EDepartment.RH, 4000,4),

                // Direction Financière
                new Salairie("778901234", "GripSous", "Bernard", new DateTime(1967, 5, 4), "Adresse GripSous", "gripsous@company.com", "7789012345", "Directeur Financier", EClassement.Directeur, EDepartment.Financier, 7000,3),
                new Salairie("889012345", "Picsou", "Julien", new DateTime(1974, 3, 7), "Adresse Picsou", "picsou@company.com", "8890123456", "Direction comptable", EClassement.ChefEquipe, EDepartment.Financier, 5000,2),
                new Salairie("990123456", "Fournier", "Antoine", new DateTime(1981, 6, 16), "Adresse Fournier", "fournier@company.com", "9901234567", "Comptable", EClassement.Comptable, EDepartment.Financier, 4000,3),
                new Salairie("1012345678", "Gautier", "Marc", new DateTime(1983, 7, 22), "Adresse Gautier", "gautier@company.com", "10123456789", "Comptable", EClassement.Comptable, EDepartment.Financier, 4000,4),
                new Salairie("1123456789", "GrosSous", "Paul", new DateTime(1979, 4, 11), "Adresse GrosSous", "grossous@company.com", "11234567890", "Contrôleur de Gestion", EClassement.ChefEquipe, EDepartment.Financier, 4500,5)
            };

            // Création de l'entreprise TransConnect
            TransConnect = new Entreprise(Dupond, salairies, Distances);
            TransConnect.Integrer(salairies[1], salairies[0]);
            TransConnect.Integrer(salairies[2], salairies[0]);
            TransConnect.Integrer(salairies[5], salairies[4]);
            TransConnect.Integrer(salairies[6], salairies[4]);
            TransConnect.Integrer(salairies[7], salairies[4]);
            TransConnect.Integrer(salairies[9], salairies[8]);
            TransConnect.Integrer(salairies[10], salairies[8]);
            TransConnect.Integrer(salairies[12], salairies[11]);
            TransConnect.Integrer(salairies[13], salairies[11]);
            TransConnect.Integrer(salairies[16], salairies[15]);
            TransConnect.Integrer(salairies[17], salairies[15]);
        }

        /// <summary>
        /// Affiche le menu principal.
        /// </summary>
        static void AfficherMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1. Gestion des Clients");
            Console.WriteLine("2. Gestion des Salariés");
            Console.WriteLine("3. Gestion des Commandes");
            Console.WriteLine("4. Afficher les Statistiques");
            Console.WriteLine("5. Gestion Tests Unitaires");
            Console.WriteLine("0. Quitter");
            Console.Write("Choisissez une option : ");
        }
        /// <summary>
        /// Gère les tests unitaires.
        /// </summary>
        static void GererTestsUnitaires()
        {
            var tests = new TestUnitaire();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tests Unitaires");
                Console.WriteLine("1. Test Dijkstra");
                Console.WriteLine("2. Test Client");
                Console.WriteLine("3. Test Commande");
                Console.WriteLine("4. Test Salarié");
                Console.WriteLine("5. Test Chauffeur");
                Console.WriteLine("6. Test Entreprise");
                Console.WriteLine("7. Exécuter tous les tests");
                Console.WriteLine("0. Retour au Menu Principal");
                Console.Write("Choisissez une option : ");
                var choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        tests.TestDijkstraCalc();
                        break;
                    case "2":
                        tests.TestAccumuler();
                        break;
                    case "3":
                        tests.TestCreerCommande();
                        break;
                    case "4":
                        tests.TestFinCommande();
                        break;
                    case "5":
                        tests.TestChauffeurDisponible();
                        break;
                    case "6":
                        tests.TestAjouterClient();
                        break;
                    case "7":
                        tests.RunTests();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
                Console.ReadLine(); // Pause après chaque test
            }
        }

        #region GérerClients
        /// <summary>
        /// Menu de gestion des clients.
        /// </summary>
        static void GererClients()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gestion des Clients");
                Console.WriteLine("1. Ajouter un Client");
                Console.WriteLine("2. Supprimer un Client");
                Console.WriteLine("3. Modifier un Client");
                Console.WriteLine("4. Afficher tous les Clients");
                Console.WriteLine("5. Afficher les Clients par ordre alphabétique");
                Console.WriteLine("6. Afficher les Clients par ville");
                Console.WriteLine("7. Afficher les Clients par montant accumulé");
                Console.WriteLine("0. Retour au Menu Principal");
                Console.Write("Choisissez une option : ");
                var choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AjouterClient();
                        break;
                    case "2":
                        SupprimerClient();
                        break;
                    case "3":
                        ModifierClient();
                        break;
                    case "4":
                        AfficherTousLesClients();
                        break;
                    case "5":
                        AfficherClientsParNom();
                        break;
                    case "6":
                        AfficherClientsParVille();
                        break;
                    case "7":
                        AfficherClientsParMontantAccumule();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
        }
        /// <summary>
        /// Affiche les clients par ordre alphabétique de nom.
        /// </summary>
        static void AfficherClientsParNom()
        {
            List<Client> clients = new List<Client>(TransConnect.Clients);
            clients.Sort((c1, c2) => c1.Nom.CompareTo(c2.Nom));
            foreach (var client in clients)
            {
                Console.WriteLine(client.ToString());
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche les clients par ville.
        /// </summary>
        static void AfficherClientsParVille()
        {
            List<Client> clients = new List<Client>(TransConnect.Clients);
            clients.Sort((c1, c2) => c1.Ville.CompareTo(c2.Ville));
            foreach (var client in clients)
            {
                Console.WriteLine(client.ToString());
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche les clients par montant accumulé des commandes.
        /// </summary>
        static void AfficherClientsParMontantAccumule()
        {
            List<Client> clients = new List<Client>(TransConnect.Clients);
            foreach (var client in clients)
            {
                client.Accumuler();
            }
            clients.Sort((c1, c2) => c2.MontantAccumule.CompareTo(c1.MontantAccumule));
            foreach (var client in clients)
            {
                Console.WriteLine(client.ToString());
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Ajoute un nouveau client.
        /// </summary>
        static void AjouterClient()
        {
            Console.Write("Entrez le N°SS du client : ");
            string nss = Console.ReadLine();
            Console.Write("Entrez le nom du client : ");
            string nom = Console.ReadLine();
            Console.Write("Entrez le prénom du client : ");
            string prenom = Console.ReadLine();
            Console.Write("Entrez la date de naissance (yyyy-mm-dd) : ");
            DateTime dateNaissance = DateTime.Parse(Console.ReadLine());
            Console.Write("Entrez l'adresse postale : ");
            string adresse = Console.ReadLine();
            Console.Write("Entrez l'email : ");
            string email = Console.ReadLine();
            Console.Write("Entrez le téléphone : ");
            string telephone = Console.ReadLine();
            Console.Write("Entrez la ville : ");
            string ville = Console.ReadLine();

            Client client = new Client(nss, nom, prenom, dateNaissance, adresse, email, telephone, ville);
            TransConnect.AjouterClient(client);
            Console.WriteLine("Client ajouté avec succès !");
            Console.ReadLine();
        }

        /// <summary>
        /// Supprime un client.
        /// </summary>
        static void SupprimerClient()
        {
            Console.Write("Entrez le N°SS du client à supprimer : ");
            string nss = Console.ReadLine();
            var client = TransConnect.Clients.FirstOrDefault(c => c.NumeroSecuriteSociale == nss);
            if (client != null)
            {
                TransConnect.Clients.Remove(client);
                Console.WriteLine("Client supprimé avec succès !");
            }
            else
            {
                Console.WriteLine("Client non trouvé !");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Modifie les informations d'un client.
        /// </summary>
        static void ModifierClient()
        {
            Console.Write("Entrez le N°SS du client à modifier : ");
            string nss = Console.ReadLine();
            var client = TransConnect.Clients.FirstOrDefault(c => c.NumeroSecuriteSociale == nss);
            if (client != null)
            {
                Console.Write("Entrez le nouveau nom (laisser vide pour ne pas changer) : ");
                string nom = Console.ReadLine();
                if (!string.IsNullOrEmpty(nom)) client.Nom = nom;

                Console.Write("Entrez le nouveau prénom (laisser vide pour ne pas changer) : ");
                string prenom = Console.ReadLine();
                if (!string.IsNullOrEmpty(prenom)) client.Prenom = prenom;

                Console.Write("Entrez la nouvelle adresse (laisser vide pour ne pas changer) : ");
                string adresse = Console.ReadLine();
                if (!string.IsNullOrEmpty(adresse)) client.AdressePostale = adresse;

                Console.Write("Entrez le nouvel email (laisser vide pour ne pas changer) : ");
                string email = Console.ReadLine();
                if (!string.IsNullOrEmpty(email)) client.AdresseEmail = email;

                Console.Write("Entrez le nouveau téléphone (laisser vide pour ne pas changer) : ");
                string telephone = Console.ReadLine();
                if (!string.IsNullOrEmpty(telephone)) client.Telephone = telephone;

                Console.Write("Entrez la nouvelle ville (laisser vide pour ne pas changer) : ");
                string ville = Console.ReadLine();
                if (!string.IsNullOrEmpty(ville)) client.Ville = ville;

                Console.WriteLine("Client modifié avec succès !");
            }
            else
            {
                Console.WriteLine("Client non trouvé !");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche tous les clients.
        /// </summary>
        static void AfficherTousLesClients()
        {
            foreach (var client in TransConnect.Clients)
            {
                Console.WriteLine(client.ToString());
            }
            Console.ReadLine();
        }
        #endregion

        #region GererSalaries
        /// <summary>
        /// Menu de gestion des salariés.
        /// </summary>
        static void GererSalaries()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gestion des Salariés");
                Console.WriteLine("1. Ajouter un Salarié");
                Console.WriteLine("2. Supprimer un Salarié");
                Console.WriteLine("3. Modifier un Salarié");
                Console.WriteLine("4. Afficher l'Organigramme");
                Console.WriteLine("0. Retour au Menu Principal");
                Console.Write("Choisissez une option : ");
                var choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AjouterSalarie();
                        break;
                    case "2":
                        SupprimerSalarie();
                        break;
                    case "3":
                        ModifierSalarie();
                        break;
                    case "4":
                        AfficherOrganigramme();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
        }

        /// <summary>
        /// Ajoute un nouveau salarié.
        /// </summary>
        static void AjouterSalarie()
        {
            Console.Write("Entrez le N°SS du salarié : ");
            string nss = Console.ReadLine();
            Console.Write("Entrez le nom du salarié : ");
            string nom = Console.ReadLine();
            Console.Write("Entrez le prénom du salarié : ");
            string prenom = Console.ReadLine();
            Console.Write("Entrez la date de naissance (yyyy-mm-dd) : ");
            DateTime dateNaissance = DateTime.Parse(Console.ReadLine());
            Console.Write("Entrez l'adresse postale : ");
            string adresse = Console.ReadLine();
            Console.Write("Entrez l'email : ");
            string email = Console.ReadLine();
            Console.Write("Entrez le téléphone : ");
            string telephone = Console.ReadLine();
            Console.Write("Entrez la date d'entrée dans la société (yyyy-mm-dd) : ");
            DateTime dateEntree = DateTime.Parse(Console.ReadLine());
            Console.Write("Entrez le poste : ");
            string poste = Console.ReadLine();
            Console.Write("Entrez le salaire : ");
            int salaire = Convert.ToInt32(Console.ReadLine());
            Console.Write("Entrez le classement (Directeur, ChefEquipe, Salairie, Chauffeur, Formation, Contrats, Comptable, Commercial, Stagier) : ");
            EClassement classement = (EClassement)Enum.Parse(typeof(EClassement), Console.ReadLine(), true);
            Console.Write("Entrez le département (Commerciale, Operation, RH, Financier) : ");
            EDepartment department = (EDepartment)Enum.Parse(typeof(EDepartment), Console.ReadLine(), true);
            Console.Write("Entrez le N°SS du salarié qui sera le parrain : ");
            string nssParrain = Console.ReadLine();

            Salairie salarie = new Salairie(nss, nom, prenom, dateNaissance, adresse, email, telephone, poste, classement, department, salaire, (DateTime.Now.Year - dateEntree.Year));
            TransConnect.Integrer(salarie, TransConnect.ChercherSalairie(nssParrain));
            Console.WriteLine("Salarié ajouté avec succès !");
            Console.ReadLine();
        }

        /// <summary>
        /// Supprime un salarié.
        /// </summary>
        static void SupprimerSalarie()
        {
            Console.Write("Entrez le N°SS du salarié à supprimer : ");
            string nss = Console.ReadLine();
            var salarie = TransConnect.ChercherSalairie(nss);
            if (salarie != null)
            {
                TransConnect.Licencier(salarie);
                Console.WriteLine("Salarié supprimé avec succès !");
            }
            else
            {
                Console.WriteLine("Salarié non trouvé !");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Modifie les informations d'un salarié.
        /// </summary>
        static void ModifierSalarie()
        {
            Console.Write("Entrez le N°SS du salarié à modifier : ");
            string nss = Console.ReadLine();
            var salarie = TransConnect.ChercherSalairie(nss);
            if (salarie != null)
            {
                Console.Write("Entrez le nouveau nom (laisser vide pour ne pas changer) : ");
                string nom = Console.ReadLine();
                if (!string.IsNullOrEmpty(nom)) salarie.Nom = nom;

                Console.Write("Entrez le nouveau prénom (laisser vide pour ne pas changer) : ");
                string prenom = Console.ReadLine();
                if (!string.IsNullOrEmpty(prenom)) salarie.Prenom = prenom;

                Console.Write("Entrez la nouvelle adresse (laisser vide pour ne pas changer) : ");
                string adresse = Console.ReadLine();
                if (!string.IsNullOrEmpty(adresse)) salarie.AdressePostale = adresse;

                Console.Write("Entrez le nouvel email (laisser vide pour ne pas changer) : ");
                string email = Console.ReadLine();
                if (!string.IsNullOrEmpty(email)) salarie.AdresseEmail = email;

                Console.Write("Entrez le nouveau téléphone (laisser vide pour ne pas changer) : ");
                string telephone = Console.ReadLine();
                if (!string.IsNullOrEmpty(telephone)) salarie.Telephone = telephone;

                Console.Write("Entrez le nouveau poste (laisser vide pour ne pas changer) : ");
                string poste = Console.ReadLine();
                if (!string.IsNullOrEmpty(poste)) salarie.Poste = poste;

                Console.Write("Entrez le nouveau salaire (laisser vide pour ne pas changer) : ");
                string salaire = Console.ReadLine();
                if (!string.IsNullOrEmpty(salaire)) salarie.Salaire = Convert.ToInt32(salaire);

                Console.WriteLine("Salarié modifié avec succès !");
            }
            else
            {
                Console.WriteLine("Salarié non trouvé !");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche l'organigramme de l'entreprise.
        /// </summary>
        static void AfficherOrganigramme()
        {
            TransConnect.AfficherOrganigramme();
            Console.ReadLine();
        }
        #endregion

        #region GererCommandes
        /// <summary>
        /// Menu de gestion des commandes.
        /// </summary>
        static void GererCommandes()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gestion des Commandes");
                Console.WriteLine("1. Créer une Commande");
                Console.WriteLine("2. Afficher les Commandes en cours");
                Console.WriteLine("3. Marquer une Commande comme finie");
                Console.WriteLine("4. Afficher les Commandes finies");
                Console.WriteLine("0. Retour au Menu Principal");
                Console.Write("Choisissez une option : ");
                var choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        CreerCommande();
                        break;
                    case "2":
                        AfficherCommandesEnCours();
                        break;
                    case "3":
                        MarquerCommandeFinie();
                        break;
                    case "4":
                        AfficherCommandesFinies();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
        }

        /// <summary>
        /// Crée une nouvelle commande.
        /// </summary>
        static void CreerCommande()
        {
            Console.Write("Entrez le N°SS du client : ");
            string nssClient = Console.ReadLine();
            var client = TransConnect.Clients.FirstOrDefault(c => c.NumeroSecuriteSociale == nssClient);
            if (client != null)
            {
                Console.Write("Entrez la destination : ");
                string destination = Console.ReadLine();

                // Demander le type de véhicule
                Console.WriteLine("Sélectionnez le type de véhicule : ");
                Console.WriteLine("1. Voiture");
                Console.WriteLine("2. Camionnette");
                Console.WriteLine("3. Camion");
                int choixVehicule = int.Parse(Console.ReadLine());

                Vehicule vehicule;
                Console.Write("Entrez l'immatriculation : ");
                string immat = Console.ReadLine();
                Console.Write("Entrez le prix de location : ");
                double prixLoue = double.Parse(Console.ReadLine());

                switch (choixVehicule)
                {
                    case 1:
                        Console.Write("Entrez le nombre de places : ");
                        int nbPlaces = int.Parse(Console.ReadLine());
                        vehicule = new Voiture(immat, prixLoue, nbPlaces);
                        break;
                    case 2:
                        Console.Write("Entrez l'usage : ");
                        string usage = Console.ReadLine();
                        vehicule = new Camionnette(immat, prixLoue, usage);
                        break;
                    case 3:
                        Console.WriteLine("Sélectionnez le type de camion : ");
                        Console.WriteLine("1. Citerne");
                        Console.WriteLine("2. Benne");
                        Console.WriteLine("3. Frigorifique");
                        int choixTypeCamion = int.Parse(Console.ReadLine());
                        TypeCamion typeCamion;
                        switch (choixTypeCamion)
                        {
                            case 1:
                                typeCamion = TypeCamion.citerne;
                                break;
                            case 2:
                                typeCamion = TypeCamion.benne;
                                break;
                            case 3:
                                typeCamion = TypeCamion.frigorifique;
                                break;
                            default:
                                Console.WriteLine("Choix de type de camion invalide !");
                                return;
                        }
                        vehicule = new Camion(immat, prixLoue, typeCamion);
                        break;
                    default:
                        Console.WriteLine("Choix de véhicule invalide !");
                        return;
                }

                Chauffeur chauffeur = TransConnect.ChauffeurDisponible();
                List<string[]> Distances = TransConnect.Distances;

                Commande commande = new Commande(client, chauffeur, vehicule, destination, Distances);
                TransConnect.AjouterCommandeEnCours(commande);

                Console.WriteLine("Commande créée avec succès !");
            }
            else
            {
                Console.WriteLine("Client non trouvé !");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche les commandes en cours.
        /// </summary>
        static void AfficherCommandesEnCours()
        {
            Console.Clear();
            Console.WriteLine("Commandes en cours :");
            for (int i = 0; i < TransConnect.CommandesEnCours.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {TransConnect.CommandesEnCours[i]}");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Marque une commande en cours comme terminée.
        /// </summary>
        static void MarquerCommandeFinie()
        {
            AfficherCommandesEnCours();
            Console.Write("Entrez le numéro de la commande à marquer comme finie : ");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;
            if (index >= 0 && index < TransConnect.CommandesEnCours.Count)
            {
                TransConnect.FinCommande(index);
                Console.WriteLine("Commande marquée comme finie avec succès !");
            }
            else
            {
                Console.WriteLine("Numéro de commande invalide !");
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche les commandes terminées.
        /// </summary>
        static void AfficherCommandesFinies()
        {
            Console.Clear();
            Console.WriteLine("Commandes finies :");
            foreach (var commande in TransConnect.CommandesFinies)
            {
                Console.WriteLine(commande.ToString());
            }
            Console.ReadLine();
        }
        #endregion

        #region AfficherStatistiques
        /// <summary>
        /// Menu pour afficher les statistiques.
        /// </summary>
        static void AfficherStatistiques()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Statistiques");
                Console.WriteLine("1. Nombre de livraisons effectuées par chauffeur");
                Console.WriteLine("2. Commandes selon une période de temps");
                Console.WriteLine("3. Moyenne des prix des commandes");
                Console.WriteLine("4. Moyenne des comptes clients");
                Console.WriteLine("5. Liste des commandes pour un client");
                Console.WriteLine("0. Retour au Menu Principal");
                Console.Write("Choisissez une option : ");
                var choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        AfficherNombreLivraisonsParChauffeur();
                        break;
                    case "2":
                        AfficherCommandesParPeriode();
                        break;
                    case "3":
                        AfficherMoyennePrixCommandes();
                        break;
                    case "4":
                        AfficherMoyenneComptesClients();
                        break;
                    case "5":
                        AfficherCommandesParClient();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
        }

        /// <summary>
        /// Affiche le nombre de livraisons effectuées par chaque chauffeur.
        /// </summary>
        static void AfficherNombreLivraisonsParChauffeur()
        {
            Console.Clear();
            Console.WriteLine("Nombre de livraisons effectuées par chauffeur :");
            foreach (var salarie in TransConnect.ChauffeursDiponible)
            {
                if (salarie is Chauffeur chauffeur)
                {
                    Console.WriteLine($"{chauffeur.Nom} {chauffeur.Prenom} : {chauffeur.NbCommandesLivrees()} livraisons");
                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche les commandes effectuées pendant une période donnée.
        /// </summary>
        static void AfficherCommandesParPeriode()
        {
            Console.Write("Entrez la date de début (yyyy-mm-dd) : ");
            DateTime dateDebut = DateTime.Parse(Console.ReadLine());
            Console.Write("Entrez la date de fin (yyyy-mm-dd) : ");
            DateTime dateFin = DateTime.Parse(Console.ReadLine());

            var commandes = TransConnect.CommandesFinies.Where(c => c.CreateDate >= dateDebut && c.CreateDate <= dateFin).ToList();

            Console.WriteLine($"Commandes du {dateDebut.ToShortDateString()} au {dateFin.ToShortDateString()} :");
            foreach (var commande in commandes)
            {
                Console.WriteLine(commande.ToString());
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche la moyenne des prix des commandes.
        /// </summary>
        static void AfficherMoyennePrixCommandes()
        {
            var moyennePrix = TransConnect.CommandesFinies.Average(c => c.Prix);
            Console.WriteLine($"La moyenne des prix des commandes est de : {moyennePrix} euros");
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche la moyenne des montants des comptes clients.
        /// </summary>
        static void AfficherMoyenneComptesClients()
        {
            foreach (var client in TransConnect.Clients)
            {
                client.Accumuler();
            }
            var moyenneMontant = TransConnect.Clients.Average(c => c.MontantAccumule);
            Console.WriteLine($"La moyenne des montants des comptes clients est de : {moyenneMontant} euros");
            Console.ReadLine();
        }

        /// <summary>
        /// Affiche les commandes pour un client spécifique.
        /// </summary>
        static void AfficherCommandesParClient()
        {
            Console.Write("Entrez le N°SS du client : ");
            string nss = Console.ReadLine();
            var client = TransConnect.Clients.FirstOrDefault(c => c.NumeroSecuriteSociale == nss);
            if (client != null)
            {
                Console.WriteLine($"Commandes pour le client {client.Nom} {client.Prenom} :");
                client.AfficherCommandes();
            }
            else
            {
                Console.WriteLine("Client non trouvé !");
            }
            Console.ReadLine();
        }
        #endregion
    }
}
