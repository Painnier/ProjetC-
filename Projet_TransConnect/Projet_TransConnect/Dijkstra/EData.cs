using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class EData
    {
        public char start; // 边的起点
        public char end;   // 边的终点
        public int weight; // 边的权重

        public EData(char start, char end, int weight)
        {
            this.start = start;
            this.end = end;
            this.weight = weight;
        }
    }
}
