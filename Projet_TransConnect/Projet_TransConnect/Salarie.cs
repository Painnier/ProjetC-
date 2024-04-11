using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class Salarie : Personne
    {
        string poste;
        double salaire;
        public Salarie(int numerosecuritesociale, string nom, string prenom, DateTime datenaissance, string adressepostale, string adresseemail, int telephone, string poste, double salaire):base(numerosecuritesociale, nom, prenom, datenaissance, adressepostale, adresseemail, telephone)
        {
            this.poste = poste;
            this.salaire = salaire;
        }
        public string Poste
        {
            get { return poste; }
            set { poste = value; }
        }
        public double Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }
        public override string ToString()
        {
            return base.ToString() + ' ' + poste + ' ' + salaire;
        }
    }
}
