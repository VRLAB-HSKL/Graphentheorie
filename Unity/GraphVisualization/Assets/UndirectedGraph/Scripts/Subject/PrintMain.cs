using System;
using System.Collections.Generic;

namespace UndirectedGraph.Scripts.Subject
{
    public class PrintMain
    {
        public static void Main(string[] args)
        {
            var graph = new Graph("Test");

            var a = graph.CreateRoot("A");
            var b = graph.CreateNode("B");
            var c = graph.CreateNode("C");
            var d = graph.CreateNode("D");
            var e = graph.CreateNode("E");
            var f = graph.CreateNode("F");
            var g = graph.CreateNode("G");
            var h = graph.CreateNode("H");
            var i = graph.CreateNode("I");
            var j = graph.CreateNode("J");
            var k = graph.CreateNode("K");
            var l = graph.CreateNode("L");
            var m = graph.CreateNode("M");
            var n = graph.CreateNode("N");
            var o = graph.CreateNode("O");
            var p = graph.CreateNode("P");

            a.AddEdge(b)
                .AddEdge(c);

            b.AddEdge(e)
                .AddEdge(d);

            c.AddEdge(f)
                .AddEdge(d);

            c.AddEdge(f)
                .AddEdge(d);

            d.AddEdge(h);

            e.AddEdge(g)
                .AddEdge(h);

            f.AddEdge(h)
                .AddEdge(i);

            g.AddEdge(j)
                .AddEdge(l);

            h.AddEdge(j)
                .AddEdge(k)
                .AddEdge(m);

            i.AddEdge(k)
                .AddEdge(n);

            j.AddEdge(o);

            k.AddEdge(p);

            l.AddEdge(o);

            m.AddEdge(o)
                .AddEdge(p);

            n.AddEdge(p);

            int?[,] adj = graph.CreateAdjacencyMatrix();

            PrintMatrix(ref adj, graph._allNodes.Count);
        }

        /// <summary>
        ///  Prints the provided matrix 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="Count"></param>
        private static void PrintMatrix(ref int?[,] matrix, int Count)
        {
            Console.Write("       ");
            for (int i = 0; i < Count; i++)
            {
                Console.Write("{0}  ", (char)('A' + i));
            }

            Console.WriteLine();

            for (int i = 0; i < Count; i++)
            {
                Console.Write("{0} | [ ", (char)('A' + i));

                for (int j = 0; j < Count; j++)
                {
                    if (i == j)
                    {
                        Console.Write(" &,");
                    }
                    else if (matrix[i, j] == null)
                    {
                        Console.Write(" .,");
                    }
                    else
                    {
                        Console.Write(" {0},", matrix[i, j]);
                    }
                }

                Console.Write(" ]\r\n");
            }

            Console.Write("\r\n");
        }

        public static void PrintList(List<List<int>> list)
        {
            foreach (var val in list)
            {
                var text = "[ ";
                val.ForEach(item => { text += item + " , "; });
                Console.WriteLine(text + " ]");
            }
        }
    }
}