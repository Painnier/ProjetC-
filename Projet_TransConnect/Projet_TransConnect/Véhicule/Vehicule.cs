using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public abstract class Vehicule
    {
        protected string immat;
        protected double prixloue;
        public Vehicule(string immat,double prixloue)
        {
            this.immat = immat;
            this.prixloue = prixloue;
        }
        public string Immat
        {
            get { return immat; }
        }
        public double PrixLoue
        {
            get { return prixloue; }
            set { prixloue = value; }
        }

        public override string ToString()
        {
            return immat.ToString();
        }
    }
}
