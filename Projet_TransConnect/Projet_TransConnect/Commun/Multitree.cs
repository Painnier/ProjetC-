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

        /// <summary>
        /// Constructeur de la classe Multitree, qui initialise l'arborescence avec un directeur général et une liste de salariés.
        /// </summary>
        /// <param name="DirecteurG">Directeur général de l'entreprise.</param>
        /// <param name="ListSalaries">Liste des salariés de l'entreprise.</param>
        public Multitree(Salarie DirecteurG, List<Salarie> ListSalaries)
        {
            this.head = new MTreeNode(DirecteurG);

            Stack<Salarie> Directeurs = new Stack<Salarie>();
            Stack<Salarie> ChefEquipes = new Stack<Salarie>();
            foreach (Salarie salarie in ListSalaries)
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
            // Ajouter automatiquement les Directeurs
            int DirecteurCount = Directeurs.Count;
            for (int i = 0; i < DirecteurCount; i++)
            {
                this.head.AjouterSalairie(Directeurs.Pop());
            }
            // Ajouter automatiquement les Chefs d'équipe
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

        /// <summary>
        /// Obtient le nœud racine de l'arborescence.
        /// </summary>
        public MTreeNode Head
        {
            get { return head; }
        }

        /// <summary>
        /// Cherche un salarié par son numéro de sécurité sociale dans l'arborescence.
        /// </summary>
        /// <param name="NSS">Numéro de sécurité sociale du salarié.</param>
        /// <param name="head">Nœud racine de l'arborescence.</param>
        /// <returns>Le nœud contenant le salarié recherché ou null si non trouvé.</returns>
        public static MTreeNode ChercherSalairie(string NSS, MTreeNode head)
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

        /// <summary>
        /// Cherche un salarié dans l'arborescence.
        /// </summary>
        /// <param name="salairie">Salarié à chercher.</param>
        /// <param name="head">Nœud racine de l'arborescence.</param>
        /// <returns>Le nœud contenant le salarié recherché ou null si non trouvé.</returns>
        public static MTreeNode ChercherSalairie(Salarie salairie, MTreeNode head)
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

        /// <summary>
        /// Intègre un nouveau salarié sous un supérieur hiérarchique.
        /// </summary>
        /// <param name="NewSalairie">Nouveau salarié à intégrer.</param>
        /// <param name="SupSalairie">Supérieur hiérarchique.</param>
        public void Integrer(Salarie NewSalairie, Salarie SupSalairie)
        {
            if (NewSalairie != null || SupSalairie != null)
            {
                ChercherSalairie(SupSalairie, this.head).AjouterSalairie(NewSalairie);
                NewSalairie.SetDateEntree();
            }
        }

        /// <summary>
        /// Licencie un salarié de l'arborescence.
        /// </summary>
        /// <param name="SalairieToLicencie">Salarié à licencier.</param>
        /// <param name="head">Nœud racine de l'arborescence.</param>
        /// <returns>Le nœud contenant le salarié licencié ou null si non trouvé.</returns>
        public static MTreeNode Licencier(Salarie SalairieToLicencie, MTreeNode head)
        {
            if (head == null)
            {
                return null;
            }

            // Si le salarié correspond, retirer le nœud courant
            if (SalairieIsExist(SalairieToLicencie, head.SSousResponsable))
            {
                foreach (MTreeNode salairie in head.SSousResponsable)
                {
                    if (salairie.Salairie.Equals(SalairieToLicencie))
                    {
                        head.SSousResponsable.Remove(salairie);
                        return salairie;
                    }
                }
            }

            // Parcourir les enfants pour chercher le salarié
            foreach (MTreeNode child in head.SSousResponsable)
            {
                MTreeNode result = Licencier(SalairieToLicencie, child);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Change le poste d'un salarié et le réintègre sous un nouveau supérieur hiérarchique.
        /// </summary>
        /// <param name="NewPoste">Nouveau poste du salarié.</param>
        /// <param name="lowsalairie">Salarié à changer de poste.</param>
        /// <param name="supsalairie">Nouveau supérieur hiérarchique.</param>
        public void ChangementPostSalairie(String NewPoste, Salarie lowsalairie, Salarie supsalairie)
        {
            Salarie temp = null;
            if (lowsalairie != null && supsalairie != null && lowsalairie.NumeroSecuriteSociale != this.head.Salairie.NumeroSecuriteSociale)
            {
                temp = lowsalairie;
                Licencier(lowsalairie, this.head);
                temp.ChangementPost(NewPoste);
                ChercherSalairie(supsalairie.NumeroSecuriteSociale, this.head).AjouterSalairie(temp);
            }
        }

        /// <summary>
        /// Affiche l'organigramme de l'entreprise.
        /// </summary>
        public void AfficherOrganigramme()
        {
            AfficherSousResponsables(this.head, 0, true, new List<bool>());
        }

        /// <summary>
        /// Affiche les sous-responsables de manière récursive.
        /// </summary>
        /// <param name="node">Nœud courant de l'arborescence.</param>
        /// <param name="niveau">Niveau du nœud courant.</param>
        /// <param name="estDernier">Indique si le nœud courant est le dernier enfant.</param>
        /// <param name="prefixeParent">Préfixe des parents pour l'affichage.</param>
        private void AfficherSousResponsables(MTreeNode node, int niveau, bool estDernier, List<bool> prefixeParent)
        {
            if (node != null)
            {
                string contenu = $"{node.Salairie.Prenom} {node.Salairie.Nom} / {node.Salairie.Poste}";
                int width = contenu.Length + 2; // Espace aux bornes
                string cadreHaut = "┌" + new string('─', width) + "┐";
                string cadreBas = "└" + new string('─', width) + "┘";
                string cadreMilieu = "| " + contenu.PadRight(width - 2) + " |";

                // Générer prefix
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

                // Afficher les nodes
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

                // Afficher les nodes récursivement
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

        /// <summary>
        /// Vérifie si un salarié existe dans une liste de nœuds.
        /// </summary>
        /// <param name="salairie">Le salarié à vérifier.</param>
        /// <param name="nodes">La liste de nœuds.</param>
        /// <returns>True si le salarié existe, sinon false.</returns>
        private static bool SalairieIsExist(Salarie salairie, List<MTreeNode> nodes)
        {
            foreach (MTreeNode node in nodes)
            {
                if (node.Salairie.Equals(salairie))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Libère les ressources de l'arborescence.
        /// </summary>
        /// <param name="head">Nœud racine de l'arborescence.</param>
        private void free_tree_r(MTreeNode head)
        {
            if (head == null)
                return;
            head = null;
        }
    }
}