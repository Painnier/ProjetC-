using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_TransConnect_TANG
{
    class Programme
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Lecture de fichier Distance.csv
            List<string[]> Distances = CSVHelper.ReadCSV(@"..\..\Distances.csv");

            // Directeur Général
            Salairie Dupond = new Salairie(123456789, "Dupond", "Jean", new DateTime(1965, 4, 23), "Adresse Dupond", "dupond@company.com", "1234567890", "Directeur Général", EClassement.Directeur, EDepartment.Operation, 8000, 20);

            List<Salairie> salairies = new List<Salairie>
            {
                // Direction Commerciale
                new Salairie(234567890, "Fiesta", "Marie", new DateTime(1970, 6, 15), "Adresse Fiesta", "fiesta@company.com", "2345678901", "Directrice Commerciale", EClassement.Directeur, EDepartment.Commerciale, 6000, 15),//0---
                new Salairie(345678901, "Forge", "Pierre", new DateTime(1975, 8, 10), "Adresse Forge", "forge@company.com", "3456789012", "Commercial", EClassement.Commercial, EDepartment.Commerciale, 4000, 10),//1
                new Salairie(456789012, "Fermi", "Sophie", new DateTime(1980, 9, 5), "Adresse Fermi", "fermi@company.com", "4567890123", "Commerciale", EClassement.Commercial, EDepartment.Commerciale, 4000, 10),//2

                // Direction des Opérations
                new Salairie(567890123, "Fetard", "Louis", new DateTime(1968, 11, 20), "Adresse Fetard", "fetard@company.com", "5678901234", "Directeur des opérations", EClassement.Directeur, EDepartment.Operation, 7000, 18),//3---
                new Salairie(678901234, "Royal", "Claude", new DateTime(1972, 1, 30), "Adresse Royal", "royal@company.com", "6789012345", "Chef Equipe", EClassement.ChefEquipe, EDepartment.Operation, 5000, 12),//4***
                new Salairie(789012345, "Romu", "Jacques", new DateTime(1985, 3, 25), "Adresse Romu", "romu@company.com", "7890123456", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 5),//5
                new Salairie(890123456, "Romi", "Pascal", new DateTime(1988, 4, 12), "Adresse Romi", "romi@company.com", "7890123456", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 4),//6
                new Salairie(901234567, "Roma", "Thierry", new DateTime(1990, 5, 18), "Adresse Roma", "roma@company.com", "9012345678", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 3),//7
                
                new Salairie(112345678, "Prince", "Claire", new DateTime(1973, 2, 22), "Adresse Prince", "prince@company.com", "1123456789", "Chef d'Equipe", EClassement.ChefEquipe, EDepartment.Operation, 5000, 11),//8***
                new Salairie(223456789, "Rome", "Isabelle", new DateTime(1986, 7, 15), "Adresse Rome", "rome@company.com", "2234567890", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 6),//9
                new Salairie(334567890, "Rimou", "Aline", new DateTime(1989, 8, 21), "Adresse Rimou", "rimou@company.com", "3345678901", "Chauffeur", EClassement.Chauffeur, EDepartment.Operation, 3000, 4),//10

                // Direction des Ressources Humaines
                new Salairie(445678901, "Joyeuse", "Lucie", new DateTime(1978, 10, 2), "Adresse Joyeuse", "joyeuse@company.com", "4456789012", "Directrice des RH", EClassement.Directeur, EDepartment.RH, 6000, 14),//11---
                new Salairie(556789012, "Couleur", "Nathalie", new DateTime(1982, 11, 9), "Adresse Couleur", "couleur@company.com", "5567890123", "Formation", EClassement.Formation, EDepartment.RH, 4000, 10),//12
                new Salairie(667890123, "TouteleMonde", "Caroline", new DateTime(1985, 12, 12), "Adresse TouteleMonde", "toutelemonde@company.com", "6678901234", "Contrats", EClassement.Contrats, EDepartment.RH, 4000, 8),//13

                // Direction Financière
                new Salairie(778901234, "GripSous", "Bernard", new DateTime(1967, 5, 4), "Adresse GripSous", "gripsous@company.com", "7789012345", "Directeur Financier", EClassement.Directeur, EDepartment.Financier, 7000, 18),//14---
                new Salairie(889012345, "Picsou", "Julien", new DateTime(1974, 3, 7), "Adresse Picsou", "picsou@company.com", "8890123456", "Direction comptable", EClassement.ChefEquipe, EDepartment.Financier, 5000, 12),//15***
                new Salairie(990123456, "Fournier", "Antoine", new DateTime(1981, 6, 16), "Adresse Fournier", "fournier@company.com", "9901234567", "Comptable", EClassement.Comptable, EDepartment.Financier, 4000, 10),//16
                new Salairie(1012345678, "Gautier", "Marc", new DateTime(1983, 7, 22), "Adresse Gautier", "gautier@company.com", "10123456789", "Comptable", EClassement.Comptable, EDepartment.Financier, 4000, 9),//17
                new Salairie(1123456789, "GrosSous", "Paul", new DateTime(1979, 4, 11), "Adresse GrosSous", "grossous@company.com", "11234567890", "Contrôleur de Gestion", EClassement.ChefEquipe, EDepartment.Financier, 4500, 11)//18***
            };
            //Création TransConnect
            Entreprise TransConnect = new Entreprise(Dupond, salairies, Distances);
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

            
            String[] Starts = Distances[0];
            String[] Ends = Distances[1];
            int[] Kms = ConvertHelper.ToIntArray(Distances[2]);
            int[] Time = ConvertHelper.HoursToMins(Distances[3]);
            String[] vexss = ConvertHelper.Villes(Starts, Ends);
            EData[] edgess = new EData[Starts.Length];

            /// <summary>
            /// Point d'entrée principal de l'application.
            /// </summary>
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuPrincipal(TransConnect));

            //"Mr Dupond" /
            //"Directeur Général"

            //"Mme Fiesta" /
            //"Directrice Commerciale"

            //"Mr Forge" /
            //"Commercial"

            //"Mme Fermi" /
            //"Commerciale"

            //"Mr Fetard" /
            //"Directeur des opérations"

            //"Mr Royal" / 
            //"Chef Equipe"

            //"Mr Romu" /
            //"Chauffeur"

            //"Mme Romi" /
            //"Chauffeur"

            //"Mr Roma" /
            //"Chauffeur"

            //"Mme Prince" / 
            //"Chef d'Equipe"

            //"Mme Rome" /
            //"Chauffeur"

            //"Mme Rimou" /
            //"Chauffeur"

            //"Mme Joyeuse" /
            //"Directrice des RH"

            //"Mme Couleur" /
            //"Formation"

            //"Mme ToutleMonde" / 
            //"Contrats"

            //"Mr GripSous" /
            //"Directeur Financier"

            //"Mr Picsou" /
            //"Direction comptable"

            //"Mme Fournier" /
            //"Comptable"

            //"Mme Gautier" /
            //"Comptable"
        }
    }
}
