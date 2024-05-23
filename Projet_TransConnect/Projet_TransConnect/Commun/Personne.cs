using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public abstract class Personne
    {
        protected string numerosecuritesociale;
        protected string nom;
        protected string prenom;
        protected DateTime datenaissance;
        protected string adressepostale;
        protected string adresseemail;
        protected string telephone;
        public Personne(string numerosecuritesociale, string nom, string prenom, DateTime datenaissance, string adressepostale, string adresseemail, string telephone) 
        {
            this.numerosecuritesociale = numerosecuritesociale;
            this.nom = nom;
            this.prenom = prenom;
            this.datenaissance = datenaissance;
            this.adresseemail = adressepostale;
            this.adresseemail = adresseemail;
            this.telephone = telephone;
        }
        public string NumeroSecuriteSociale
        {
            get { return numerosecuritesociale; }
            set { this.numerosecuritesociale = value; }
        }
        public string Nom
        {
            get { return nom; }
            set { this.nom = value; }
        }
        public string Prenom
        { 
            get { return prenom; } 
            set {  this.prenom = value; } 
        }
        public DateTime DateNaissance
        {
            get { return datenaissance; }
            set { this.datenaissance = value; }
        }
        public string AdressePostale
        {
            get { return adressepostale; }
            set { this.adressepostale = value; }
        }
        public string AdresseEmail
        { 
            get { return adresseemail; }
            set { this.adresseemail = value;}
        }
        public string Telephone
        {
            get { return telephone; }
            set { this.telephone = value; }
        }
        public override string ToString()
        {
            return numerosecuritesociale + " - " + nom + " - " + prenom + " - " + datenaissance + " - " + adressepostale + " - " + adresseemail + " - " + telephone;
        }
    }
}
