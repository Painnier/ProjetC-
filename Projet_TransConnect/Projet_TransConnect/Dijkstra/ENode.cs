using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class ENode
    {
        public int ivex;       // 该边所指向的顶点的位置
        public int weight;     // 该边的权
        public ENode nextEdge; // 指向下一条弧的指针
    }
}
