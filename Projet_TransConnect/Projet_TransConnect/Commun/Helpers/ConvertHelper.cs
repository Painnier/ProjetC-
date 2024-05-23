using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    /// <summary>
    /// Classe utilitaire pour les conversions diverses.
    /// </summary>
    public class ConvertHelper
    {
        /// <summary>
        /// Convertit un tableau de chaînes de caractères en tableau d'entiers.
        /// </summary>
        /// <param name="lignes">Le tableau de chaînes de caractères à convertir.</param>
        /// <returns>Un tableau d'entiers convertis.</returns>
        public static int[] ToIntArray(String[] lignes)
        {
            int[] lignesint = new int[lignes.Length];
            for (int i = 0; i < lignes.Length; i++)
            {
                if (lignes[i] != null)
                {
                    lignesint[i] = Convert.ToInt32(lignes[i]);
                }
            }
            return lignesint;
        }

        /// <summary>
        /// Convertit un tableau de chaînes de caractères représentant des durées en heures et minutes en un tableau de minutes.
        /// </summary>
        /// <param name="Hours">Le tableau de chaînes de caractères à convertir.</param>
        /// <returns>Un tableau d'entiers représentant les durées en minutes.</returns>
        public static int[] HoursToMins(String[] Hours)
        {
            int[] minutes = new int[Hours.Length];
            for (int i = 0; i < Hours.Length; i++)
            {
                if (Hours[i] != null)
                {
                    if (Hours[i].ToLower().Contains("h"))
                    {
                        String[] hour = Hours[i].ToLower().Split('h');
                        if (hour[1] == "")
                        {
                            minutes[i] = Convert.ToInt32(hour[0]) * 60;
                        }
                        else
                        {
                            minutes[i] = Convert.ToInt32(hour[0]) * 60 + Convert.ToInt32(hour[1]);
                        }
                    }
                    else
                    {
                        String[] hour = Hours[i].ToLower().Split('m');
                        minutes[i] = Convert.ToInt32(hour[0]);
                    }
                }
            }
            return minutes;
        }

        /// <summary>
        /// Fusionne deux tableaux de chaînes de caractères en éliminant les doublons.
        /// </summary>
        /// <param name="starts">Le premier tableau de chaînes de caractères.</param>
        /// <param name="ends">Le second tableau de chaînes de caractères.</param>
        /// <returns>Un tableau de chaînes de caractères fusionné et sans doublons.</returns>
        public static String[] Villes(String[] starts, String[] ends)
        {
            String[] Starts = starts.Distinct().ToArray();
            String[] Ends = ends.Distinct().ToArray();
            return Starts.Union(Ends).ToArray();
        }
    }
}
