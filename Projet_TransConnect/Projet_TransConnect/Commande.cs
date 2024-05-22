using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    delegate int CalPrix();

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
        EData[] edgess;

        CalPrix FacteurKms, FacteurTemps; // Temps en minute

        Client client;
        string start, end;
        double prix;
        Chauffeur chauffeur;
        Vehicule vehicule;
        int KmParcouru, TempsUtilise = 0; // Temps en minute

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
            FacteurKms = KmsParcouru;
            FacteurTemps = TempsParcouru;
            this.prix = FacteurKms() * PayByKms + chauffeur.Anciennete * FacteurTemps() * PayByTimes;
            String[] Starts = Distances[0];
            String[] Ends = Distances[1];
            int[] Kms = ConvertHelper.ToIntArray(Distances[2]);
            int[] Times = ConvertHelper.HoursToMins(Distances[3]);
            String[] vexss = ConvertHelper.Villes(Starts, Ends);
            EData[] edgess = new EData[Starts.Length];
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
            return "Client : " + client.ToString() + ", prix : " + prix.ToString() + ", chauffeur : [" + chauffeur.ToString() + "], create date : " + createdate.ToString() + ", véhicule : " + vehicule + ", trajet : " + start + " to " + end;
        }

        /// <summary>
        /// Marque la commande comme effectuée en définissant la date de finalisation à maintenant.
        /// </summary>
        public void Effectuee()
        {
            this.finaldate = DateTime.Now;
        }

        /// <summary>
        /// Calcule le nombre de kilomètres parcourus pour la commande.
        /// </summary>
        /// <returns>Nombre de kilomètres parcourus.</returns>
        private int KmsParcouru()
        {
            for (int i = 0; i < Starts.Length; i++)
            {
                this.edgess[i] = new EData(this.Starts[i], this.Ends[i], this.Kms[i]);
            }
            Dijkstra Dij = new Dijkstra(this.vexss, this.edgess);
            Dij.CalcDijkstra(this.start, this.end);
            return Dij.Distance;
        }

        /// <summary>
        /// Calcule le temps parcouru en minutes pour la commande.
        /// </summary>
        /// <returns>Temps parcouru en minutes.</returns>
        private int TempsParcouru()
        {
            for (int i = 0; i < Starts.Length; i++)
            {
                this.edgess[i] = new EData(this.Starts[i], this.Ends[i], this.Times[i]);
            }
            Dijkstra Dij = new Dijkstra(this.vexss, this.edgess);
            Dij.CalcDijkstra(this.start, this.end);
            return Dij.Distance;
        }
    }
}
