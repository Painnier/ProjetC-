using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public abstract class Personne
    {
        protected int numerosecuritesociale;
        protected string nom;
        protected string prenom;
        protected DateTime datenaissance;
        protected string adressepostale;
        protected string adresseemail;
        protected int telephone;
        public Personne(int numerosecuritesociale, string nom, string prenom, DateTime datenaissance, string adressepostale, string adresseemail,int telephone) 
        {
            this.numerosecuritesociale = numerosecuritesociale;
            this.nom = nom;
            this.prenom = prenom;
            this.datenaissance = datenaissance;
            this.adresseemail = adressepostale;
            this.adresseemail = adresseemail;
            this.telephone = telephone;
        }
        public int NumeroSecuriteSociale
        {
            get { return numerosecuritesociale; }
            //set { this.numerosecuritesociale = value; }
        }
        public string Nom
        {
            get { return nom; }
            set { this.nom = value; }
        }
        public string Prenom
        { 
            get { return nom; } 
            //set {  this.nom = value; } 
        }
        public DateTime DateNaissance
        {
            get { return datenaissance; }
            //set { this.datenaissance = value; }
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
        public int Telephone
        {
            get { return telephone; }
            set { this.telephone = value; }
        }
        public override string ToString()
        {
            return numerosecuritesociale + ' ' + nom + ' ' + prenom + ' ' + datenaissance + ' ' + adressepostale + ' ' + adresseemail + ' ' + telephone;
        }
    }
}
