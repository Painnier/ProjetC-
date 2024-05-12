using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class CSVHelper
    {
        public static List<string[]> ReadCSV(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.GetEncoding(936));
            List<string> lignes_temp = new List<string>();
            List<string[]> lignes = new List<string[]>();

            int ColonneLenght = 0;

            string str = "";

            while (str != null)
            {
                str = sr.ReadLine();
                if (str != null)
                {
                    lignes_temp.Add(Regex.Replace(str, "[" + Regex.Escape("\"") + "]", ""));
                    ColonneLenght++;
                }
            }

            String[] temp = lignes_temp[0].Split(',');
            int LigneLenght = temp.Length;

            for (int i = 0; i < LigneLenght; i++)
            {
                String[] resultat = new String[ColonneLenght];
                for (int j = 0; j < ColonneLenght; j++)
                {
                    string[] cases = lignes_temp[j].Split(',');
                    resultat[j] = cases[i];
                }
                lignes.Add(resultat);
            }
            sr.Close();
            return lignes;
        }
    }
}
