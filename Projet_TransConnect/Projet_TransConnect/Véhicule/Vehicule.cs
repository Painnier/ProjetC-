using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public abstract class Vehicule
    {
        protected int immat;
        public Vehicule(int immat)
        {
            immat = 0;
        }
        public int Immat
        {
            get { return immat; }
        }
        public override string ToString()
        {
            return immat.ToString();
        }
    }
}
