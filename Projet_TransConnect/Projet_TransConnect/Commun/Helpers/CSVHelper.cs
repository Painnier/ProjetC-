using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    /// <summary>
    /// Classe utilitaire pour la lecture de fichiers CSV.
    /// </summary>
    public class CSVHelper
    {
        /// <summary>
        /// Lit un fichier CSV et retourne une liste de tableaux de chaînes de caractères représentant les colonnes.
        /// </summary>
        /// <param name="path">Le chemin du fichier CSV à lire.</param>
        /// <returns>Une liste de tableaux de chaînes de caractères représentant les colonnes du fichier CSV.</returns>
        public static List<string[]> ReadCSV(string path)
        {
            // Ouverture du fichier en mode lecture
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding(936)); // Lecture avec un encodage spécifique
            List<string> lignes_temp = new List<string>(); // Liste temporaire pour stocker les lignes lues
            List<string[]> lignes = new List<string[]>(); // Liste pour stocker les colonnes du fichier

            int ColonneLenght = 0; // Variable pour stocker le nombre de colonnes
            string str = ""; // Variable pour stocker les lignes lues

            // Lecture de chaque ligne du fichier
            while (str != null)
            {
                str = sr.ReadLine();
                if (str != null)
                {
                    // Suppression des guillemets et ajout de la ligne à la liste temporaire
                    lignes_temp.Add(Regex.Replace(str, "[" + Regex.Escape("\"") + "]", ""));
                    ColonneLenght++;
                }
            }

            // Séparation de la première ligne en colonnes
            String[] temp = lignes_temp[0].Split(',');
            int LigneLenght = temp.Length; // Nombre de colonnes

            // Conversion des lignes en colonnes
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
            // Fermeture du lecteur de flux
            sr.Close();
            return lignes;
        }
    }
}
