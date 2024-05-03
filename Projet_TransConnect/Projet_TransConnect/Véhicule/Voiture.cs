using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class Voiture : Vehicule
    {
        int nb_passages;
        public Voiture(int immat, int nb_passage) : base(immat)
        {
            this.nb_passages = nb_passage;
        }
        public override string ToString()
        {
            return base.ToString() + ' ' + nb_passages.ToString();
        }
    }
}