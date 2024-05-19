using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_TransConnect_TANG
{
    public class Dijkstra
    {
        public static int MAX = int.MaxValue;
        public String[] vexss;
        public int mEdgNum;    // 边的数量
        public VNode[] mVexs;  // 顶点数组
        int distance;


        /*
         * 创建图
         *
         * 参数说明：
         *     vexs  -- 顶点数组
         *     edges -- 边
         */
        public Dijkstra(String[] vexs, EData[] edges)
        {
            this.distance = 0;
            this.vexss = vexs;
            // 初始化"顶点数"和"边数"
            int vlen = vexs.Length;
            int elen = edges.Length;

            // 初始化"顶点"
            mVexs = new VNode[vlen];
            for (int i = 0; i < mVexs.Length; i++)
            {
                mVexs[i] = new VNode();
                mVexs[i].data = vexs[i];
                mVexs[i].firstEdge = null;
            }

            // 初始化"边"
            mEdgNum = elen;
            for (int i = 0; i < elen; i++)
            {
                // 读取边的起始顶点和结束顶点
                String c1 = edges[i].start;
                String c2 = edges[i].end;
                int weight = edges[i].weight;

                // 读取边的起始顶点和结束顶点
                int p1 = GetPosition(c1);
                int p2 = GetPosition(c2);
                // 初始化node1
                ENode node1 = new ENode();
                node1.ivex = p2;
                node1.weight = weight;
                // 将node1链接到"p1所在链表的末尾"
                if (mVexs[p1].firstEdge == null)
                    mVexs[p1].firstEdge = node1;
                else
                    LinkLast(mVexs[p1].firstEdge, node1);
                // 初始化node2
                ENode node2 = new ENode();
                node2.ivex = p1;
                node2.weight = weight;
                // 将node2链接到"p2所在链表的末尾"
                if (mVexs[p2].firstEdge == null)
                    mVexs[p2].firstEdge = node2;
                else
                    LinkLast(mVexs[p2].firstEdge, node2);
            }
        }
        public int Distance
        {
            get { return distance; }
        }

        /*
         * 将node节点链接到list的最后
         */
        private void LinkLast(ENode list, ENode node)
        {
            ENode p = list;

            while (p.nextEdge != null)
                p = p.nextEdge;
            p.nextEdge = node;
        }

        /*
         * 返回ch位置
         */
        private int GetPosition(String ch)
        {
            for (int i = 0; i < mVexs.Length; i++)
                if (mVexs[i].data == ch)
                    return i;
            return -1;
        }
        /*
        * Dijkstra最短路径。
        * 即，统计图中"起点D"到其它各个顶点的最短路径。
        *
        * 参数说明：
        *       vs -- 起始顶点(start vertex)。
        *     prev -- 前驱顶点数组。即，prev[i]的值是"起点D"到"顶点i"的最短路径所经历的全部顶点中，位于"顶点i"之前的那个顶点。
        *     dist -- 长度数组。即，dist[i]是"起点D"到"顶点i"的最短路径的长度。
        */
        public int[] CalcDijkstra(String start, String end)
        {
            // flag[i]=true表示"起点D"到"顶点i"的最短路径已成功获取。
            bool[] flag = new bool[mVexs.Length];
            int vs = Array.IndexOf(this.vexss, start);
            int[] dist = new int[this.mVexs.Length];
            int[] chemin = new int[this.mVexs.Length];

            // 初始化
            for (int i = 0; i < mVexs.Length; i++)
            {
                flag[i] = false;            // 顶点i的最短路径还没获取到。
                chemin[i] = 0;                // 顶点i的前驱顶点为0。
                dist[i] = GetWeight(vs, i); // 顶点i的最短路径为"起点D"到"顶点i"的权。
            }

            // 对"起点D"自身进行初始化
            flag[vs] = true;
            dist[vs] = 0;

            // 遍历mVexs.Length-1次；每次找出一个顶点的最短路径。
            int k = 0;
            for (int i = 1; i < mVexs.Length; i++)
            {
                // 寻找当前最小的路径
                // 即，在未获取最短路径的顶点中，找到离起点D最近的顶点(k)。
                int min = MAX;
                for (int j = 0; j < mVexs.Length; j++)
                {
                    if (flag[j] == false && dist[j] < min)
                    {
                        min = dist[j];
                        k = j;//找到当前最短的路线
                    }
                }
                // 标记"顶点k"为已经获取到最短路径
                flag[k] = true;

                // 更新当前最短路径和前驱顶点
                // 即，更新"未获取最短路径的顶点的最短路径和前驱顶点"。
                for (int j = 0; j < mVexs.Length; j++)
                {
                    int tmp = GetWeight(k, j);//找到当前最短的路线与其他路线的距离
                    tmp = (tmp == MAX ? MAX : (min + tmp)); // 防止溢出
                    if (flag[j] == false && (tmp < dist[j]))
                    {
                        dist[j] = tmp;
                        chemin[j] = k;
                    }
                }
            }
            
            this.distance = dist[Array.IndexOf(this.vexss, end)];
            return chemin;
        }

        /*
         * 获取边<start, end>的权值；若start和end不是连通的，则返回无穷大。
         */
        private int GetWeight(int start, int end)
        {
            if (start == end)
                return 0;

            ENode node = mVexs[start].firstEdge;
            while (node != null)
            {
                if (end == node.ivex)
                    return node.weight;
                node = node.nextEdge;
            }
            return MAX;
        }
    }
}
