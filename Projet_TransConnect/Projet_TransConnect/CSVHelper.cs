using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect
{
    public class CSVHelper
    {
        public String[] ReadCSV(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding(936)); 
            List<string> ligne = new List<string>();

            int ColonneLenght = 0;

            string str = "";

            while (str != null)
            {
                str = sr.ReadLine();
                ligne.Add(str);
                ColonneLenght++;
            }

            String[] resultat = new String[ligne[0].Length];

            for(int i = 0; i < ColonneLenght; i++)
            {
                ligne[i] = str.Split(',');
            }
            sr.Close();
            return [];
        }
    }
}
