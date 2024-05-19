using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class Multitree
    {
        MTreeNode head;

        
        public Multitree(Salairie DirecteurG, List<Salairie> ListSalaries)
        {
            this.head = new MTreeNode(DirecteurG);
            Stack<Salairie> Directeurs = new Stack<Salairie>();
            Stack<Salairie> ChefEquipes = new Stack<Salairie>();
            Stack<Salairie> Salairies = new Stack<Salairie>();

            foreach (Salairie salarie in ListSalaries)
            {
                if (salarie.Classement == EClassement.Directeur)
                {
                    Directeurs.Push(salarie);
                }
                else if (salarie.Classement == EClassement.ChefEquipe)
                {
                    ChefEquipes.Push(salarie);
                }
                else
                {
                    Salairies.Push(salarie);
                }
            }

            for (int i = 0; i < Directeurs.Count; i++)
            {
                head.AjouterSalairie(Directeurs.Pop());
            }

            for (int i = 0; i < ChefEquipes.Count; i++)
            {
                foreach (MTreeNode directeur in head.SSousResponsable)
                {
                    if (ChefEquipes.Peek().Department == directeur.Salairie.Department)
                    {
                        directeur.AjouterSalairie(ChefEquipes.Pop());
                    }
                }
            }
        }
        public static MTreeNode ChercherSalairie(int NSS, MTreeNode head)
        {
            MTreeNode temp = null;
            if (head != null)
            {
                if (head.Salairie.NumeroSecuriteSociale == NSS)
                {
                    temp = head; //Si c'est match
                }
                else //Sinon chercher les fils
                {
                    for (int i = 0; i < head.NChildren && temp == null; i++)
                    {
                        temp = ChercherSalairie(NSS, head.SSousResponsable[i]);
                    }
                }
            }
            return temp;
        }
        public static MTreeNode ChercherSalairie(Salairie salairie, MTreeNode head)
        {
            MTreeNode temp = null;
            if (head != null)
            {
                if (salairie.Equals(head.Salairie))
                {
                    temp = head; //Si c'est match
                }
                else //Sinon chercher les fils
                {
                    for (int i = 0; i < head.NChildren && temp == null; i++)
                    {
                        temp = ChercherSalairie(salairie, head.SSousResponsable[i]);
                    }
                }
            }
            return temp;
        }
        public bool Integrer(Salairie NewSalairie, Salairie OldSalairie)
        {
            if (NewSalairie != null|| OldSalairie != null)
            {
                if (NewSalairie.Classement == EClassement.Stagier || NewSalairie.Department == OldSalairie.Department)
                {
                    ChercherSalairie(OldSalairie.NumeroSecuriteSociale, this.head).AjouterSalairie(NewSalairie);
                    NewSalairie.SetDateEntree();
                }
                else 
                { 
                    return false; 
                }
                return true;
            }
            return false;
        }
        public static MTreeNode Licencier(Salairie SalairieToLicencie, MTreeNode head)
        {
            MTreeNode temp = null;
            if (head != null)
            {
                if (SalairieIsExist(SalairieToLicencie, head.SSousResponsable))
                { 
                    foreach (MTreeNode salairie in head.SSousResponsable)
                    {
                        if (salairie.Salairie.Equals(SalairieToLicencie))
                        {
                            head.SSousResponsable.Remove(salairie);
                        }
                    }
                }
                else //Sinon chercher les fils
                {
                    for (int i = 0; i < head.NChildren && temp == null; i++)
                    {
                        temp = Licencier(SalairieToLicencie, head.SSousResponsable[i]);
                    }
                }
            }
            return temp;
        }
        public void ChangementPostSalairie(String NewPoste, Salairie lowsalairie, Salairie supsalairie)
        {
            Salairie temp = null;
            if (lowsalairie != null && supsalairie != null)
            {
                temp = lowsalairie;
                Licencier(lowsalairie,this.head);
                temp.ChangementPost(NewPoste);
                ChercherSalairie(supsalairie.NumeroSecuriteSociale, this.head).AjouterSalairie(temp);

            }
        }
        private static bool SalairieIsExist(Salairie salairietoseach,List<MTreeNode> SSousResponsable)
        {
            foreach(MTreeNode salairie in SSousResponsable)
            {
                if (salairie.Salairie.Equals(salairietoseach))
                {
                    return true;
                }
            }
            return false;
        }
        private void free_tree_r(MTreeNode head)
        {
            if (head == null)
                return;
            head = null;
        }
    }

}
