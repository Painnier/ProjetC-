using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class MTreeNode
    {
        private Salairie salarie;
        private int nChildren;
        private int level = -1;
        List<MTreeNode> children;

        public MTreeNode(Salairie salarie)
        {
            this.salarie = salarie;
            children = null;
        }
        public MTreeNode()
        {
            this.salarie = null;
            children = null;
        }

        #region Property
        public Salairie Salairie
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
        /// <summary>
        /// Salairiés sous résponsable
        /// </summary>
        public List<MTreeNode> SSousResponsable
        {
            get { return children; }
            set { children = value; }
        }
        #endregion
        #region Fonction public
        public bool AjouterSalairie(Salairie salairie)
        {
            if(children == null)
            {
                children = new List<MTreeNode>();
            }
            if (this != null && salairie != null)
            {
                this.children.Add(new MTreeNode(salairie));
                return true;
            }
            return false;
        }
        #endregion
    }
}
