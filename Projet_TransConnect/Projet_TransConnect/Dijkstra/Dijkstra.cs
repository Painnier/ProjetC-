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
        public static int MAX = int.MaxValue;
        public String[] vexss;
        public int mEdgNum;    // Nombre de bords
        public VNode[] mVexs;  // Tableau de sommets
        int distance;

        /// <summary>
        /// Constructeur de la classe Dijkstra.
        /// </summary>
        /// <param name="vexs">Tableau des noms des sommets.</param>
        /// <param name="edges">Tableau des bords du graphe.</param>
        public Dijkstra(String[] vexs, EData[] edges)
        {
            this.distance = 0;
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

                // Position des sommets dans le tableau
                int p1 = GetPosition(c1);
                int p2 = GetPosition(c2);

                // Initialisation du premier nœud
                ENode node1 = new ENode();
                node1.ivex = p2;
                node1.weight = weight;

                // Ajout du nœud au sommet de départ
                if (mVexs[p1].firstEdge == null)
                    mVexs[p1].firstEdge = node1;
                else
                    LinkLast(mVexs[p1].firstEdge, node1);

                // Initialisation du second nœud (pour le graphe non orienté)
                ENode node2 = new ENode();
                node2.ivex = p1;
                node2.weight = weight;

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
        /// <returns>Tableau des sommets précédents sur le chemin le plus court.</returns>
        public int[] CalcDijkstra(String start, String end)
        {
            bool[] flag = new bool[mVexs.Length]; // Indique si le plus court chemin vers un sommet a été trouvé
            int vs = Array.IndexOf(this.vexss, start);
            int[] dist = new int[this.mVexs.Length]; // Tableau des distances
            int[] chemin = new int[this.mVexs.Length]; // Tableau des sommets précédents

            // Initialisation
            for (int i = 0; i < mVexs.Length; i++)
            {
                flag[i] = false;
                chemin[i] = 0;
                dist[i] = GetWeight(vs, i);
            }

            // Initialisation pour le sommet de départ
            flag[vs] = true;
            dist[vs] = 0;

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

                // Met à jour les distances pour les sommets adjacents
                for (int j = 0; j < mVexs.Length; j++)
                {
                    int tmp = GetWeight(k, j);
                    tmp = (tmp == MAX ? MAX : (min + tmp));
                    if (!flag[j] && (tmp < dist[j]))
                    {
                        dist[j] = tmp;
                        chemin[j] = k;
                    }
                }
            }

            this.distance = dist[Array.IndexOf(this.vexss, end)];
            return chemin;
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
    }
}
