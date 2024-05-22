using System;

namespace Projet_TransConnect_TANG
{
    /// <summary>
    /// Représente un nœud d'arête dans un graphe.
    /// </summary>
    public class ENode
    {
        /// <summary>
        /// L'indice du sommet auquel cette arête est connectée.
        /// </summary>
        public int ivex;

        /// <summary>
        /// Le poids de l'arête.
        /// </summary>
        public int weight;

        /// <summary>
        /// Pointeur vers la prochaine arête dans la liste.
        /// </summary>
        public ENode nextEdge;
    }
}
