using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class ConvertHelper
    {
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
        public static String[] Villes(String[] starts, String[] ends)
        {
            String[] Starts = starts.Distinct().ToArray();
            String[] Ends = ends.Distinct().ToArray();
            return Starts.Union(Ends).ToArray();
        }
    }
}
