using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class EData
    {
        public String start; // 边的起点
        public String end;   // 边的终点
        public int weight; // 边的权重

        public EData(String start, String end, int weight)
        {
            this.start = start;
            this.end = end;
            this.weight = weight;
        }
    }
}
