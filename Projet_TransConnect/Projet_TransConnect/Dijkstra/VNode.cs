using System;

namespace Projet_TransConnect_TANG
{
    /// <summary>
    /// Représente un nœud de sommet dans un graphe.
    /// </summary>
    public class VNode
    {
        /// <summary>
        /// Les informations du sommet.
        /// </summary>
        public String data;

        /// <summary>
        /// Pointeur vers la première arête attachée à ce sommet.
        /// </summary>
        public ENode firstEdge;
    }
}