using Projet_TransConnect_TANG.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class Salairie : Personne
    {
        protected string poste;
        protected int salaire;
        protected DateTime dateentree;
        protected int anciennete;
        protected EClassement classement;
        protected EDepartment department;

        public Salairie(string numerosecuritesociale, string nom, string prenom, DateTime datenaissance, string adressepostale, string adresseemail, string telephone, string poste, EClassement classement, EDepartment department, int salaire, int anciennete) : base(numerosecuritesociale, nom, prenom, datenaissance, adressepostale, adresseemail, telephone)
        {
            this.poste = poste;
            this.salaire = salaire;
            this.classement = classement;
            this.department = department;
            this.anciennete = anciennete;
            this.anciennete = anciennete;
        }
        #region Property
        public string Poste
        {
            get { return poste; }
            set { poste = value; }
        }
        public int Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }
        public EClassement Classement
        {
            get { return classement; }
            set { classement = value; }
        }
        public EDepartment Department
        {
            get { return department; }
            set { department = value; }
        }
        #endregion
        #region Fonction public
        public override string ToString()
        {
            return base.ToString() + ' ' + poste + ' ' + salaire;
        }
        public void SetDateEntree()
        {
            this.dateentree = DateTime.Now;
        }
        public virtual void FinCommande(Commande commande) { }
        public void ChangementPost(String newposte)
        {
            if (this.poste ==  newposte)
            {
                this.poste = newposte;
            }
        }
        #endregion
    }
}
