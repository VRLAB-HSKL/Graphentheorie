﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace GraphContent
{
    public class GraphMono : MonoBehaviour
    {
        public Graph _Graph;
        public string Name;
        public string rootName;
        public Text matrixText;
        

        private void Start()
        {
            _Graph = new Graph(Name);
            _Graph.CreateRoot(rootName);
            ProvideInputs();

        }

        private void LateUpdate()
        {
            Debug.Log(_Graph.ToString());
        }
        public void ProvideInputs()
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

            // o - Already added

            // p - Already added

            int?[,] adj = graph.CreateAdjacencyMatrix(); // We're going to implement that down below

            PrintMatrix(ref adj, graph._allNodes.Count); // We're going to implement that down below
        }

        private void PrintMatrix(ref int?[,] matrix, int Count)
        {
            matrixText.text += "   ";
            for (int i = 0; i < Count; i++)
            {
                matrixText.text +=  "  "+ (char)('A' + i);
            }

            matrixText.text += "\r\n"; 

            for (int i = 0; i < Count; i++)
            {
                matrixText.text += " | [ "+ (char)('A' + i);

                for (int j = 0; j < Count; j++)
                {
                    if (i == j)
                    {
                        matrixText.text +=" &,";
                    }
                    else if (matrix[i, j] == null)
                    {
                        matrixText.text +=" .,";
                    }
                    else
                    {
                        matrixText.text += " ,"+ matrix[i, j];
                    }
                }

                matrixText.text += " ]\r\n";
            }

            matrixText.text += "\r\n";
        }
    }
}