using System;
using System.Collections.Generic;
using Manager;
using Subject;
using UnityEngine;
using UnityEngine.UI;

namespace GraphContent
    /// <summary>
    /// This Class could be attached to the Gameobject which enables the given parameter.
    /// </summary>
{
    public class GraphMono : MonoBehaviour
    {
        public Graph _Graph;
        public string Name;
        public string rootName;
        public Text matrixText;
        public string JsonFileName;
        private DataManagerSingleton dataMgmt = DataManagerSingleton.Instance;

        private Graph graph;
        

        private void Start()
        {
            _Graph = new Graph(Name);
            _Graph.CreateRoot(rootName);
            ProvideInputs();
            var temp = dataMgmt.ParseInFile();
            Debug.Log("Name : "+temp.Name + " , Size : " + temp.Size);

            Matrix m = new Matrix();
            m.Name = "Test Matrix";
            m.Size = 10;
            var getM = dataMgmt.SaveJsonData(m);
            Debug.Log(getM);
            dataMgmt.SaveJsonFile(m);

            JsonDataSerializer jsonDataSerializer = new JsonDataSerializer(JsonFileName);
            jsonDataSerializer.SaveJsonFile(graph.CreateAdjacencyList());
            
            Debug.Log(jsonDataSerializer.LoadJsonFile());
        }
        
        public void ProvideInputs()
        {
            graph = new Graph(Name);

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
            
            //For the List Print
            Console.WriteLine("--------------List Print----------------");
            List<List<int>> res = graph.CreateAdjacencyList();
            PrintMain.PrintList(res);
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