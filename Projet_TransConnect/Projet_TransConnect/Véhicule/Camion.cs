using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class Camion : Vehicule
    {
        public enum TypeCamion
        {
            citerne,
            benne,
            frigorifique
        }
        private TypeCamion type;
        public Camion(int immat, TypeCamion type) : base(immat)
        {
            this.type = type;
        }
        public TypeCamion GetTypeCamion()
        { 
            return type; 
        }
        public override string ToString()
        {
            return base.ToString() + ' ' + type.ToString();
        }
    }
}
