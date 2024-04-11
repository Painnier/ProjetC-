using Projet_TransConnect_TANG;
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
        private Stack<Noeud> t_stack;
        private Queue<Noeud> queue_t = new Queue<Noeud>();

        private static Noeud search_node_r(Salarie salarie, Noeud head)
        {
            Noeud temp = null;
            if (head != null)
            {
                if (salarie.Equals(head.Salarie))
                {
                    temp = head; //Si c'est match
                }
                else //Sinon cherche les branches fils
                {
                    for (int i = 0; i < head.NChildren && temp == null; i++)
                    {
                        temp = search_node_r(salarie, head.Children[i]);
                    }
                }
            }
            return temp;
        }

        
        public static void read_file(ref Noeud head, string filePath)
        {
            Noeud temp = null;
            int n;
            string name, child;
            using (StreamReader sr = new StreamReader(filePath))
            {
                string strLine = string.Empty;
                while ((strLine = sr.ReadLine()) != null)
                {
                    string[] strings = strLine.Split(' ');
                    name = strings[0];
                    n = int.Parse(strings[1]);
                    if (head == null) 
                    {
                        temp = head = new Noeud();
                        temp.Salarie = name; 
                    }
                    else
                    {
                        temp = search_node_r(name, head);
                    }
                    
                    temp.NChildren = n;
                    for (int i = 0; i < n; i++)
                    {
                        child = strings[i + 2];
                        temp.Children.Add(new Noeud());
                        temp.Children[i].Name = child;
                    }
                }
            }

        }

        private static void f1(Noeud head)
        {
            Noeud tNoeud;
            Queue<Noeud> queue = new Queue<Noeud>(100); 
            Stack<Noeud> stack = new Stack<Noeud>(100); 
            head.Level = 0; 
            queue.Enqueue(head);

            while (queue.Count != 0) 
            {
                tNoeud = queue.Dequeue(); 
                for (int i = 0; i < tNoeud.NChildren; i++)
                {
                    tNoeud.Children[i].Level = tNoeud.Level + 1; 
                    queue.Enqueue(tNoeud.Children[i]); 
                }
                stack.Push(tNoeud); 
            }

            while (stack.Count != 0)
            {
                tNoeud = stack.Pop();  
                System.Diagnostics.Debug.WriteLine("   {0} {1}", tNoeud.Level, tNoeud.Name);
            }
        }

        private static void f2(Noeud head, string str, ref string strBest, int level)
        {
            if (head == null) return;
            var tmp = str + head.Name;

            if (head.NChildren == 0)
            {
                if (strBest == null || tmp.Length > strBest.Length)
                {
                    strBest = tmp;
                }
            }
            for (var i = 0; i < head.NChildren; i++)
            {
                f2(head.Children[i], tmp, ref strBest, level + 1);
            }
        }

        private void free_tree_r(Noeud head)
        {
            if (head == null)
                return;
            head = null;
        }
    }

}
