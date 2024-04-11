using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class Noeud
    {
        private Salarie salarie;
        private int nChildren;
        private int level = -1;
        List<Noeud> children;

        public Noeud()
        {
            children = new List<Noeud>();
        }

        public Salarie Salarie
        {
            get { return salarie; }
            set { salarie = value; }
        }

        public int NChildren
        {
            get { return nChildren; }
            set { nChildren = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        public List<Noeud> Children
        {
            get { return children; }
            set { children = value; }
        }
    }
}
