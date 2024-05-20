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
            }
            //Ajouter automatiquement les Directeur
            int DirecteurCount = Directeurs.Count;
            for (int i = 0; i < DirecteurCount; i++)
            {
                this.head.AjouterSalairie(Directeurs.Pop());
            }
            //Ajouter automatiquement les Chefs d'équipe
            int ChefEquipesCount = ChefEquipes.Count;
            for (int i = 0; i < ChefEquipesCount; i++)
            {
                foreach (MTreeNode directeur in this.head.SSousResponsable)
                {
                    if (ChefEquipes.Count != 0)
                    {
                        if (ChefEquipes.Peek().Department == directeur.Salairie.Department)
                        {
                            directeur.AjouterSalairie(ChefEquipes.Pop());
                        }
                    }
                }
            }
        }
        public MTreeNode Head
        {  
            get { return head; } 
        }
        public static MTreeNode ChercherSalairie(int NSS, MTreeNode head)
        {
            if (head == null)
            {
                return null;
            }
            // Si le numéro de sécurité sociale correspond, retourner le nœud courant
            if (head.Salairie.NumeroSecuriteSociale == NSS)
            {
                return head;
            }
            // Parcourir les enfants pour chercher le salarié
            foreach (MTreeNode child in head.SSousResponsable)
            {
                MTreeNode result = ChercherSalairie(NSS, child);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
        public static MTreeNode ChercherSalairie(Salairie salairie, MTreeNode head)
        {
            if (head == null)
            {
                return null;
            }
            // Si le salarié correspond, retourner le nœud courant
            if (salairie.Equals(head.Salairie))
            {
                return head;
            }
            // Parcourir les enfants pour chercher le salarié
            foreach (MTreeNode child in head.SSousResponsable)
            {
                MTreeNode result = ChercherSalairie(salairie, child);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
        public bool Integrer(Salairie NewSalairie, Salairie SupSalairie)
        {
            if (NewSalairie != null|| SupSalairie != null)
            {
                if (NewSalairie.Classement == EClassement.Stagier || NewSalairie.Department == SupSalairie.Department)
                {
                    ChercherSalairie(SupSalairie, this.head).AjouterSalairie(NewSalairie);
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
        public void AfficherOrganigramme()
        {
            AfficherSousResponsables(this.head, 0, true, new List<bool>());
        }

        private void AfficherSousResponsables(MTreeNode node, int niveau, bool estDernier, List<bool> prefixeParent)
        {
            if (node != null)
            {
                // 动态计算当前节点框架的宽度
                string contenu = $"{node.Salairie.Prenom} {node.Salairie.Nom} / {node.Salairie.Poste}";
                int width = contenu.Length - contenu.Length/4; // 加上两边空格和边框
                string cadreHaut = "┌" + new string('─', width) + "┐";
                string cadreBas = "└" + new string('─', width) + "┘";
                string cadreMilieu = "| " + contenu.PadRight(width - 2) + " |";

                // 生成前缀
                string prefixe = "";
                for (int i = 0; i < prefixeParent.Count; i++)
                {
                    if (i == prefixeParent.Count - 1)
                    {
                        prefixe += prefixeParent[i] ? "    " : "│   ";
                    }
                    else
                    {
                        prefixe += prefixeParent[i] ? "    " : "│   ";
                    }
                }

                // 打印当前节点
                if (niveau > 0)
                {
                    Console.WriteLine(prefixe + (estDernier ? "    " : "│   ") + cadreHaut);
                    Console.WriteLine(prefixe + (estDernier ? "└── " : "├── ") + cadreMilieu);
                    Console.WriteLine(prefixe + (estDernier ? "    " : "│   ") + cadreBas);
                }
                else
                {
                    Console.WriteLine(cadreHaut);
                    Console.WriteLine(cadreMilieu);
                    Console.WriteLine(cadreBas);
                }

                // 递归打印子节点
                if (node.SSousResponsable != null)
                {
                    prefixeParent.Add(estDernier);
                    for (int i = 0; i < node.SSousResponsable.Count; i++)
                    {
                        bool dernier = (i == node.SSousResponsable.Count - 1);
                        AfficherSousResponsables(node.SSousResponsable[i], niveau + 1, dernier, new List<bool>(prefixeParent));
                    }
                    prefixeParent.RemoveAt(prefixeParent.Count - 1);
                }
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
