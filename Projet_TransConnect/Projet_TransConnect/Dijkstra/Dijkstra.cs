using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    /// <summary>
    /// Représente l'algorithme de Dijkstra pour le calcul des plus courts chemins.
    /// </summary>
    public class Dijkstra
    {
        static int MAX = int.MaxValue;
        String[] vexss;
        int mEdgNum;    // Nombre de bords
        VNode[] mVexs;  // Tableau de sommets
        int distance;
        int timeTaken;
        List<string> chemin;

        /// <summary>
        /// Constructeur de la classe Dijkstra.
        /// </summary>
        /// <param name="vexs">Tableau des noms des sommets.</param>
        /// <param name="edges">Tableau des bords du graphe avec distances.</param>
        /// <param name="edgesTime">Tableau des bords du graphe avec temps.</param>
        public Dijkstra(String[] vexs, EData[] edges, EData[] edgesTime)
        {
            this.distance = 0;
            this.timeTaken = 0;
            this.vexss = vexs;
            // Initialisation du nombre de sommets et de bords
            int vlen = vexs.Length;
            int elen = edges.Length;

            // Initialisation des sommets
            mVexs = new VNode[vlen];
            for (int i = 0; i < mVexs.Length; i++)
            {
                mVexs[i] = new VNode();
                mVexs[i].data = vexs[i];
                mVexs[i].firstEdge = null;
            }

            // Initialisation des bords
            mEdgNum = elen;
            for (int i = 0; i < elen; i++)
            {
                // Lecture du sommet de départ et du sommet de fin pour chaque bord
                String c1 = edges[i].start;
                String c2 = edges[i].end;
                int weight = edges[i].weight;
                int time = edgesTime[i].weight;

                // Position des sommets dans le tableau
                int p1 = GetPosition(c1);
                int p2 = GetPosition(c2);

                // Initialisation du premier nœud
                ENode node1 = new ENode();
                node1.ivex = p2;
                node1.weight = weight;
                node1.time = time;

                // Ajout du nœud au sommet de départ
                if (mVexs[p1].firstEdge == null)
                    mVexs[p1].firstEdge = node1;
                else
                    LinkLast(mVexs[p1].firstEdge, node1);

                // Initialisation du second nœud (pour le graphe non orienté)
                ENode node2 = new ENode();
                node2.ivex = p1;
                node2.weight = weight;
                node2.time = time;

                // Ajout du nœud au sommet de fin
                if (mVexs[p2].firstEdge == null)
                    mVexs[p2].firstEdge = node2;
                else
                    LinkLast(mVexs[p2].firstEdge, node2);
            }
        }

        /// <summary>
        /// Obtient la distance calculée par l'algorithme de Dijkstra.
        /// </summary>
        public int Distance
        {
            get { return distance; }
        }

        /// <summary>
        /// Obtient le temps pris pour le parcours le plus court calculé par l'algorithme de Dijkstra.
        /// </summary>
        public int TimeTaken
        {
            get { return timeTaken; }
        }
        public List<string> Chemin
        { 
            get { return chemin; } 
        }

        /// <summary>
        /// Ajoute un nœud à la fin de la liste des bords.
        /// </summary>
        /// <param name="list">Liste des bords.</param>
        /// <param name="node">Nœud à ajouter.</param>
        private void LinkLast(ENode list, ENode node)
        {
            ENode p = list;
            while (p.nextEdge != null)
                p = p.nextEdge;
            p.nextEdge = node;
        }

        /// <summary>
        /// Retourne la position d'un sommet dans le tableau.
        /// </summary>
        /// <param name="ch">Nom du sommet.</param>
        /// <returns>Position du sommet dans le tableau.</returns>
        private int GetPosition(String ch)
        {
            for (int i = 0; i < mVexs.Length; i++)
                if (mVexs[i].data == ch)
                    return i;
            return -1;
        }

        /// <summary>
        /// Calcule les plus courts chemins depuis un sommet donné en utilisant l'algorithme de Dijkstra.
        /// </summary>
        /// <param name="start">Nom du sommet de départ.</param>
        /// <param name="end">Nom du sommet de fin.</param>
        /// <returns>Temps pris pour le parcours le plus court.</returns>
        public void CalcDijkstra(String start, String end)
        {
            bool[] flag = new bool[mVexs.Length]; // Indique si le plus court chemin vers un sommet a été trouvé
            int vs = Array.IndexOf(this.vexss, start);
            int[] dist = new int[this.mVexs.Length]; // Tableau des distances
            int[] time = new int[this.mVexs.Length]; // Tableau des temps
            int[] prev = new int[this.mVexs.Length]; // Tableau des sommets précédents

            // Initialisation
            for (int i = 0; i < mVexs.Length; i++)
            {
                flag[i] = false;
                dist[i] = GetWeight(vs, i);
                time[i] = GetTime(vs, i);
                if (dist[i] < MAX)
                    prev[i] = vs;
                else
                    prev[i] = -1;
            }

            // Initialisation pour le sommet de départ
            flag[vs] = true;
            dist[vs] = 0;
            time[vs] = 0;

            // Recherche des plus courts chemins pour les autres sommets
            for (int i = 1; i < mVexs.Length; i++)
            {
                int min = MAX;
                int k = 0;

                // Recherche du sommet avec la plus petite distance
                for (int j = 0; j < mVexs.Length; j++)
                {
                    if (!flag[j] && dist[j] < min)
                    {
                        min = dist[j];
                        k = j;
                    }
                }

                // Marque le sommet comme trouvé
                flag[k] = true;

                // Met à jour les distances et les temps pour les sommets adjacents
                for (int j = 0; j < mVexs.Length; j++)
                {
                    int tmpDist = GetWeight(k, j);
                    int tmpTime = GetTime(k, j);
                    tmpDist = (tmpDist == MAX ? MAX : (dist[k] + tmpDist));
                    tmpTime = (tmpTime == MAX ? MAX : (time[k] + tmpTime));
                    if (!flag[j] && (tmpDist < dist[j]))
                    {
                        dist[j] = tmpDist;
                        time[j] = tmpTime;
                        prev[j] = k;
                    }
                }
            }

            this.distance = dist[Array.IndexOf(this.vexss, end)];
            this.timeTaken = time[Array.IndexOf(this.vexss, end)];
            this.chemin = ReconstructPath(vs, Array.IndexOf(this.vexss, end), prev);
        }
        public override string ToString()
        {
            foreach (string ville in this.Chemin)
            {
                Console.Write(" --- [ " + ville + " ] ");
            }
            return "Distance : " + distance + "Time Taken : " + timeTaken  + "Chemin : " + chemin;
        }

        /// <summary>
        /// Obtient le poids de l'arête entre deux sommets.
        /// </summary>
        /// <param name="start">Sommet de départ.</param>
        /// <param name="end">Sommet d'arrivée.</param>
        /// <returns>Poids de l'arête entre les deux sommets, ou MAX si les sommets ne sont pas connectés.</returns>
        private int GetWeight(int start, int end)
        {
            if (start == end)
                return 0;

            ENode node = mVexs[start].firstEdge;
            while (node != null)
            {
                if (end == node.ivex)
                    return node.weight;
                node = node.nextEdge;
            }
            return MAX;
        }

        /// <summary>
        /// Obtient le temps de l'arête entre deux sommets.
        /// </summary>
        /// <param name="start">Sommet de départ.</param>
        /// <param name="end">Sommet d'arrivée.</param>
        /// <returns>Temps de l'arête entre les deux sommets, ou MAX si les sommets ne sont pas connectés.</returns>
        private int GetTime(int start, int end)
        {
            if (start == end)
                return 0;

            ENode node = mVexs[start].firstEdge;
            while (node != null)
            {
                if (end == node.ivex)
                    return node.time;
                node = node.nextEdge;
            }
            return MAX;
        }
        private List<string> ReconstructPath(int start, int end, int[] prev)
        {
            List<string> path = new List<string>();
            int at = end;
            while (at != -1)
            {
                path.Add(vexss[at]);
                if (at == start) break; // Ajout de cette ligne pour éviter une boucle infinie
                at = prev[at];
            }
            path.Reverse();
            return path;
        }
    }
}
