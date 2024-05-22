using System;

namespace Projet_TransConnect_TANG
{
    /// <summary>
    /// Représente une arête dans un graphe avec un point de départ, un point d'arrivée et un poids.
    /// </summary>
    public class EData
    {
        /// <summary>
        /// Le point de départ de l'arête.
        /// </summary>
        public String start;

        /// <summary>
        /// Le point d'arrivée de l'arête.
        /// </summary>
        public String end;

        /// <summary>
        /// Le poids de l'arête.
        /// </summary>
        public int weight;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="EData"/>.
        /// </summary>
        /// <param name="start">Le point de départ de l'arête.</param>
        /// <param name="end">Le point d'arrivée de l'arête.</param>
        /// <param name="weight">Le poids de l'arête.</param>
        public EData(String start, String end, int weight)
        {
            this.start = start;
            this.end = end;
            this.weight = weight;
        }
    }
}
