using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class TestUnitaire
    {
        List<string[]> distances = CSVHelper.ReadCSV(@"..\..\Distances.csv");
        #region Dijkstra
        public void TestDijkstraCalc()
        {
            String[] vexs = { "A", "B", "C", "D" };
            EData[] edges = {
                new EData("A", "B", 1),
                new EData("B", "C", 2),
                new EData("A", "C", 4),
                new EData("C", "D", 1),
            };
            EData[] edgesTime = {
                new EData("A", "B", 10),
                new EData("B", "C", 20),
                new EData("A", "C", 30),
                new EData("C", "D", 10),
            };

            Dijkstra dijkstra = new Dijkstra(vexs, edges, edgesTime);
            dijkstra.CalcDijkstra("A", "D");

            if (dijkstra.Distance == 4 && dijkstra.TimeTaken == 40)
            {
                dijkstra.ToString();
                Console.WriteLine("TestDijkstraCalc Passed");
            }
            else
            {
                Console.WriteLine("TestDijkstraCalc Failed");
            }
        }
        #endregion


        #region Client
        public void TestAccumuler()
        {
            var client = new Client("456", "Nom", "Prenom", new DateTime(1990, 1, 1), "Adresse", "email@example.com", "1234567890", "Paris");

            var chauffeur = new Chauffeur("789", "Chauffeur", "Test", new DateTime(1985, 3, 25), "Adresse", "email@example.com", "1234567890", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 5);
            var camionnette = new Camionnette("789QDZAQD", 188.2, "usage");
            var commande1 = new Commande(client, chauffeur, camionnette, "Rouen", distances) { Prix = 100 };
            var commande2 = new Commande(client, chauffeur, camionnette, "Rouen", distances) { Prix = 200 };

            client.FinCommande(commande1);
            client.FinCommande(commande2);
            client.Accumuler();

            if (client.MontantAccumule == 300)
            {
                Console.WriteLine(client.MontantAccumule.ToString());
                Console.WriteLine( "TestAccumuler Passed");
            }
            else
            {
                Console.WriteLine("TestAccumuler Failed");
            }
        }
        #endregion

        #region Commande
        public void TestCreerCommande()
        {
            var directeur = new Salairie("123", "Dupond", "Jean", new DateTime(1965, 4, 23), "Adresse", "email@example.com", "1234567890", "Directeur Général", EClassement.Directeur, EDepartment.Operation, 8000, 20);
            var chauffeur = new Chauffeur("789", "Chauffeur", "Test", new DateTime(1985, 3, 25), "Adresse", "email@example.com", "1234567890", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 5);
            var entreprise = new Entreprise(directeur, new List<Salairie> { chauffeur }, distances);
            var client = new Client("456", "Nom", "Prenom", new DateTime(1990, 1, 1), "Adresse", "email@example.com", "1234567890", "Paris");
            entreprise.AjouterClient(client);

            var vehicule = new Voiture("1234", 50, 4);
            var commande = new Commande(client, chauffeur, vehicule, "La Rochelle", distances);
            entreprise.AjouterCommandeEnCours(commande);

            if (entreprise.CommandesEnCours.Contains(commande))
            {
                Console.WriteLine(client.ToString());
                Console.WriteLine();
                Console.WriteLine(chauffeur.ToString());
                Console.WriteLine();
                Console.WriteLine(commande.ToString());
                Console.WriteLine();
                Console.WriteLine("TestCreerCommande Passed");
            }
            else
            {
                Console.WriteLine("TestCreerCommande Failed");
            }
        }
        #endregion

        #region Salairié
        public void TestFinCommande()
        {
            Salairie directeur = new Salairie("123", "Dupond", "Jean", new DateTime(1965, 4, 23), "Adresse", "email@example.com", "1234567890", "Directeur Général", EClassement.Directeur, EDepartment.Operation, 8000, 20);
            Salairie salairie = new Chauffeur("789", "Nom", "Test", new DateTime(1985, 3, 25), "Adresse", "email@example.com", "1234567890", "Poste", EClassement.Directeur, EDepartment.Operation, 3000, 5);
            Chauffeur chauffeur = new Chauffeur("123", "Chauffeur", "Prenom", new DateTime(1990, 1, 1), "Adresse", "email@example.com", "1234567890", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 4000, 5);

            Entreprise entreprise = new Entreprise(directeur, new List<Salairie> { salairie }, distances);

            Console.WriteLine("Before :");
            entreprise.AfficherOrganigramme();
            entreprise.Integrer(chauffeur, directeur);
            entreprise.Licencier(salairie);

            if (entreprise.ChercherSalairie(salairie) == null)
            {
                Console.WriteLine("After :");
                entreprise.AfficherOrganigramme();
                Console.WriteLine("TestFinCommande Passed");
            }
            else
            {
                Console.WriteLine("TestFinCommande Failed");
            }
        }
        #endregion

        #region Chauffeur
        public void TestChauffeurDisponible()
        {
            Client client = new Client("456", "ClientNom", "ClientPrenom", new DateTime(1990, 1, 1), "Adresse", "email@example.com", "1234567890", "Paris");
            var vehicule = new Voiture("1234", 50, 4);
            var directeur = new Salairie("123", "Dupond", "Jean", new DateTime(1965, 4, 23), "Adresse", "email@example.com", "1234567890", "Directeur Général", EClassement.Directeur, EDepartment.Operation, 8000, 20);
            var chauffeur = new Chauffeur("789", "Chauffeur", "Test", new DateTime(1985, 3, 25), "Adresse", "email@example.com", "1234567890", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 5);
            var entreprise = new Entreprise(directeur, new List<Salairie> { chauffeur }, distances);
            Commande commande = new Commande(client, chauffeur, vehicule, "La Rochelle", distances);

            var disponible = entreprise.ChauffeurDisponible();
            Console.WriteLine("Before :");
            Console.WriteLine(chauffeur.NbCommandesLivrees());
            chauffeur.FinCommande(commande);

            if (disponible != null && disponible.NumeroSecuriteSociale == "789")
            {
                Console.WriteLine("After :");
                Console.WriteLine(chauffeur.NbCommandesLivrees());
                Console.WriteLine(disponible.ToString());
                Console.WriteLine("TestChauffeurDisponible Passed");
            }
            else
            {
                Console.WriteLine("TestChauffeurDisponible Failed");
            }
        }
        #endregion

        #region Entreprise
        public void TestAjouterClient()
        {
            var directeur = new Salairie("123", "Dupond", "Jean", new DateTime(1965, 4, 23), "Adresse", "email@example.com", "1234567890", "Directeur Général", EClassement.Directeur, EDepartment.Operation, 8000, 20);
            var entreprise = new Entreprise(directeur, new List<Salairie> {}, distances);
            var client = new Client("456", "Nom", "Prenom", new DateTime(1990, 1, 1), "Adresse", "email@example.com", "1234567890", "Ville");

            Console.WriteLine("Nombre de cliens Before :");
            Console.WriteLine(entreprise.Clients.Count);
            entreprise.AjouterClient(client);

            if (entreprise.Clients.Contains(client))
            {
                Console.WriteLine("Nombre de cliens After :");
                Console.WriteLine(entreprise.Clients.Count);
                Console.WriteLine("TestAjouterClient Passed");
            }
            else
            {
                Console.WriteLine("TestAjouterClient Failed");
            }
        }
        #endregion

        // Méthode pour exécuter tous les tests
        public void RunTests()
        {
            TestDijkstraCalc();
            TestAccumuler();
            TestCreerCommande();
            TestFinCommande();
            TestChauffeurDisponible();
            TestAjouterClient();
            // Ajouter d'autres appels de tests ici
            Console.WriteLine("Tests finished.");
        }
    }
}
