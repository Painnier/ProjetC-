using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    delegate void CalPrix();
    public class Commande
    {
        String[] Starts, Ends;
        int[] Kms,Times;
        String[] vexss;
        EData[] edgess;

        CalPrix PrixFinal;

        Client client;
        string start, end;
        double prix;
        Salarie chauffeur;
        Vehicule vehicule;
        int KmParcouru=0;
        int TempsMinKmParcouru = 0;

        DateTime createdate, finaldate; 

        double PayByKms = 5.0;
        double PayByTimes = 0.3;

        public Commande(Client client, Salarie chauffeur, Vehicule vehicule, string start, String end, List<string[]> Distances)
        {
            this.createdate = DateTime.Now;
            this.client = client;
            this.chauffeur = chauffeur;
            this.vehicule = vehicule;
            this.start = start;
            this.end = end;
            PrixFinal = PrixKms;
            String[] Starts = Distances[0];
            String[] Ends = Distances[1];
            int[] Kms = ConvertHelper.ToIntArray(Distances[2]);
            int[] Times = ConvertHelper.HoursToMins(Distances[3]);
            String[] vexss = ConvertHelper.Villes(Starts, Ends);
            EData[] edgess = new EData[Starts.Length];
        }
        public double Prix
        {
            get { return prix; }
        }
        public DateTime CreateDate
        {
            get { return createdate; }
        }
        public DateTime FinalDate
        {
            get { return finaldate; }
            set { finaldate = value; }
        }
        public Client Client 
        { 
            get { return client; } 
        }
        public Salarie Chauffeur
        {
            get { return chauffeur; }
            set { chauffeur = value; }
        }
        public Vehicule Vehicule
        {
            get { return vehicule; }
            set { vehicule = value; }
        }
        public override string ToString()
        {
            return "Client : " + client.ToString() + ", prix : " + prix.ToString() + ", chauffeur : [" + chauffeur.ToString() + "], create date : " + createdate.ToString() + ", véhicule : " + vehicule + ", trafic : " +  start + " to " + end;
        }
        private void PrixKms()
        {
            for (int i = 0; i < Starts.Length; i++)
            {
                edgess[i] = new EData(Starts[i], Ends[i], Kms[i]);
            }
            Dijkstra Dij = new Dijkstra(vexss, edgess);
            this.prix = Convert.ToDouble(Dij.CalcDijkstra(start, end))*PayByTimes;
            this.KmParcouru = Dij.CalcDijkstra(start, end);
        }
        public void Effectuee()
        {
            PrixFinal();
            this.finaldate = DateTime.Now;
            client.Accumuler();
        }
    }
}
    