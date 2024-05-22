using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    /// <summary>
    /// Représente une commande dans l'entreprise.
    /// </summary>
    public class Commande
    {
        private static int NextNumeroCommande = 0;

        int numerocommande;
        String[] Starts, Ends;
        int[] Kms, Times;
        String[] vexss;
        EData[] edgess, edgetime;
        Dijkstra Dij;

        Client client;
        string start, end;
        double prix;
        Chauffeur chauffeur;
        Vehicule vehicule;
        int KmParcouru, TempsUtilise; // Temps en minute

        DateTime createdate, finaldate;

        double PayByKms = 5;
        double PayByTimes = 0.3;

        /// <summary>
        /// Constructeur de la classe Commande.
        /// </summary>
        /// <param name="client">Client associé à la commande.</param>
        /// <param name="chauffeur">Chauffeur assigné à la commande.</param>
        /// <param name="vehicule">Véhicule utilisé pour la commande.</param>
        /// <param name="end">Destination de la commande.</param>
        /// <param name="Distances">Liste des distances entre différents points.</param>
        public Commande(Client client, Chauffeur chauffeur, Vehicule vehicule, string end, List<string[]> Distances)
        {
            this.numerocommande = NextNumeroCommande++;
            this.createdate = DateTime.Now;
            this.client = client;
            this.chauffeur = chauffeur;
            this.vehicule = vehicule;
            this.start = client.Ville;
            this.end = end;
            this.Starts = Distances[0];
            this.Ends = Distances[1];
            this.Kms = ConvertHelper.ToIntArray(Distances[2]);
            this.Times = ConvertHelper.HoursToMins(Distances[3]);
            this.vexss = ConvertHelper.Villes(Starts, Ends);
            this.edgess = new EData[Starts.Length];
            for (int i = 0; i < Starts.Length; i++)
            {
                this.edgess[i] = new EData(this.Starts[i], this.Ends[i], this.Kms[i]);
            }
            this.edgetime = new EData[Starts.Length];
            for (int i = 0; i < Starts.Length; i++)
            {
                this.edgetime[i] = new EData(this.Starts[i], this.Ends[i], this.Times[i]);
            }

            this.Dij = new Dijkstra(this.vexss, this.edgess, this.edgetime);
            this.Dij.CalcDijkstra(client.Ville, end);
            this.KmParcouru = this.Dij.Distance;
            this.TempsUtilise = this.Dij.TimeTaken;
            this.prix = KmParcouru * PayByKms + chauffeur.Anciennete * TempsUtilise * PayByTimes + vehicule.PrixLoue;
        }

        #region Property
        /// <summary>
        /// Obtient le numéro de la commande.
        /// </summary>
        public int NumeroCommande
        {
            get { return numerocommande; }
        }

        /// <summary>
        /// Obtient le point de départ de la commande.
        /// </summary>
        public string Start { get { return start; } }

        /// <summary>
        /// Obtient la destination de la commande.
        /// </summary>
        public string End { get { return end; } }

        /// <summary>
        /// Obtient le prix de la commande.
        /// </summary>
        public double Prix
        {
            get { return this.prix; }
            set { this.prix = value; }
        }

        /// <summary>
        /// Obtient la date de création de la commande.
        /// </summary>
        public DateTime CreateDate
        {
            get { return this.createdate; }
        }

        /// <summary>
        /// Obtient ou définit la date de finalisation de la commande.
        /// </summary>
        public DateTime FinalDate
        {
            get { return this.finaldate; }
            set { finaldate = value; }
        }

        /// <summary>
        /// Obtient le client associé à la commande.
        /// </summary>
        public Client Client
        {
            get { return this.client; }
        }

        /// <summary>
        /// Obtient ou définit le chauffeur assigné à la commande.
        /// </summary>
        public Chauffeur Chauffeur
        {
            get { return this.chauffeur; }
            set { chauffeur = value; }
        }

        /// <summary>
        /// Obtient ou définit le véhicule utilisé pour la commande.
        /// </summary>
        public Vehicule Vehicule
        {
            get { return this.vehicule; }
            set { vehicule = value; }
        }
        #endregion

        /// <summary>
        /// Retourne une représentation sous forme de chaîne de caractères de la commande.
        /// </summary>
        /// <returns>Chaîne de caractères représentant la commande.</returns>
        public override string ToString()
        {
            return "Numero Commande : " + numerocommande +  "Client : " + client.ToString() + ", prix : " + prix.ToString() + ", chauffeur : [" + chauffeur.ToString() + "], create date : " + createdate.ToString() + ", véhicule : " + vehicule + ", trajet : " + start + " to " + end + "Temps total utilisé: " + Dij.TimeTaken + "\r\n" + "Chemin : " + AfficherCheminParcouru();
        }
        /// <summary>
        /// Calcule et retourne le chemin parcouru avec chaque ville encadrée.
        /// </summary>
        /// <returns>Le chemin parcouru sous forme de chaîne de caractères.</returns>
        public string AfficherCheminParcouru()
        {
            StringBuilder cheminEncadre = new StringBuilder();
            cheminEncadre.Append("Chemin parcouru: ");
            foreach (string ville in this.Dij.Chemin)
            {
                cheminEncadre.Append(" --- [ " + ville + " ] ");
            }
            return cheminEncadre.ToString();
        }
        /// <summary>
        /// Marque la commande comme effectuée en définissant la date de finalisation à maintenant.
        /// </summary>
        public void Effectuee()
        {
            this.finaldate = DateTime.Now;
        }
    }
}
