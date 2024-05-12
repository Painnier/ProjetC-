using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    class Programme
    {
        public static void Main(string[] args)
        {
            //Salarie mrDupond = new Salarie(123456789, "Dupond", "Mr", new DateTime(1970, 5, 15), "95000", "mr.dupond@example.com", 0123456789, "Directeur Général", 10000.00);
            //Salarie mmeFiesta = new Salarie(987654321, "Fiesta", "Mme", new DateTime(1985, 10, 20), "95000", "mme.fiesta@example.com", 0987654321, "Directrice Commerciale", 8000.00);
            //Salarie mrForge = new Salarie(456123789, "Forge", "Mr", new DateTime(1980, 7, 25), "95000", "mr.forge@example.com", 0654321098, "Commercial", 6000.00);

            //char[] vexss = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
            //EData[] edgess = {
            // 起点 终点 权重 
            //new EData('A', 'B', 12),
            //new EData('A', 'F', 16),
            //new EData('A', 'G', 14),
            //new EData('B', 'C', 10),
            //new EData('B', 'F',  7),
            //new EData('C', 'D',  3),
            //new EData('C', 'E',  5),
            //new EData('C', 'F',  6),
            //new EData('D', 'E',  4),
            //new EData('E', 'F',  2),
            //new EData('E', 'G',  8),
            //new EData('F', 'G',  9),
            //};

            //Dijkstra pGf = new Dijkstra(vexss, edgess);
            // dijkstra算法获取"起点D"到其它各个顶点的最短距离
            //int[] prev = new int[pGf.mVexs.Length];
            //int[] dist = new int[pGf.mVexs.Length];
            //pGf.CalcDijkstra(3, prev, dist);

            List<string[]> Distances = CSVHelper.ReadCSV("\\C#\\Projet_TransConnect_Gihub\\Projet_TransConnect\\Projet_TransConnect\\Distances.csv");
            String[] Starts = Distances[0];
            String[] Ends = Distances[1];
            int[] Kms = ConvertHelper.ToIntArray(Distances[2]);
            int[] Time = ConvertHelper.HoursToMins(Distances[3]);
            String[] vexss = ConvertHelper.Villes(Starts, Ends);
            EData[] edgess = new EData[Starts.Length];

            for (int i = 0; i < Starts.Length; i++)
            {
                edgess[i] = new EData(Starts[i], Ends[i], Time[i]);
            }
            Dijkstra pGf = new Dijkstra(vexss, edgess);

            pGf.CalcDijkstra("Paris", "La Rochelle");


            Console.WriteLine("1");
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
