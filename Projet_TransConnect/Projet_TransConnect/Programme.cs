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
            //Salarie mrDupond = new Salarie(123456789, "Dupond", "Mr", new DateTime(1970, 5, 15), "95000", "mr.dupond@example.com", 0123456789, "Directeur Général", 10000.00);
            //Salarie mmeFiesta = new Salarie(987654321, "Fiesta", "Mme", new DateTime(1985, 10, 20), "95000", "mme.fiesta@example.com", 0987654321, "Directrice Commerciale", 8000.00);
            //Salarie mrForge = new Salarie(456123789, "Forge", "Mr", new DateTime(1980, 7, 25), "95000", "mr.forge@example.com", 0654321098, "Commercial", 6000.00);

            List<string[]> Distances = CSVHelper.ReadCSV(@"..\..\Distances.csv");
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
            Application.Run(new MenuPrincipal());

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
