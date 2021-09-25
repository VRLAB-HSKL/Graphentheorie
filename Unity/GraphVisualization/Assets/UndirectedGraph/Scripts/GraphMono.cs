﻿using System;
using System.Collections.Generic;
using GraphGen;
using Manager;
using Subject;
using UndirectedGraph.Scripts.Subject;
using UndirectedGraph.Scripts.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Vector3 = System.Numerics.Vector3;

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
        private Graph graphSmall;

        [SerializeField] private Create3DNode node3D;

        [SerializeField] private AnswersManager answersManager;

        private void Awake()
        {
            _Graph = new Graph(Name);
            _Graph.CreateRoot(rootName);
            ProvideInputs();
            node3D.CreateGraphFromData();
            answersManager.CreateAllRandomAnswers();
        }

        /// <summary>
        /// Here are components written only to Test 
        /// </summary>
        private void Start()
        {
            var temp = dataMgmt.ParseInFile();
            // Debug.Log("Name : "+temp.Name + " , Size : " + temp.Size);

            Matrix m = new Matrix();
            m.Name = "Test Matrix";
            m.Size = 10;
            //var getM = dataMgmt.SaveJsonData(m);
            // Debug.Log(getM);
            dataMgmt.SaveJsonFile(m);
            //
            // JsonDataSerializer jsonDataSerializer = new JsonDataSerializer(JsonFileName);
            // jsonDataSerializer.SaveJsonFile(graph.CreateAdjacencyList());

            // Debug.Log(jsonDataSerializer.LoadJsonFile(JsonFileName));

            ProvideSmallInputs();
            // JsonDataSerializer dataSerializer = new JsonDataSerializer("smallGraph");
            // dataSerializer.SaveJsonFile(graphSmall.CreateAdjacencyList());

            //save MatrixData
            JsonDataSerializer dsMatrixData = new JsonDataSerializer(JsonFileName);
            MatrixData matrixData = new MatrixData();
            matrixData.name = "TestMatrix";
            matrixData.nodes = graphSmall.CreateAdjacencyList();
            matrixData.nodeNames = new List<string> { "Primasens", "Zweibrücken", "Kaiserslautern" };

            matrixData.nodePositions.Add(new Vector3(1, 0, 1));
            matrixData.nodePositions.Add(new Vector3(2, 0, 1));
            matrixData.nodePositions.Add(new Vector3(3, 0, 2));
            matrixData.nodePositions.Add(new Vector3(4, 0, 2));
            matrixData.nodePositions.Add(new Vector3(5, 0, 3));
            matrixData.nodePositions.Add(new Vector3(5, 0, 4));
            var listData = new Dictionary<string, MatrixData>();
            matrixData.startingPoint = "D";
            listData.Add("A1", matrixData);
            matrixData.name = "TestMatrix2";
            matrixData.startingPoint = "A";
            listData.Add("A2", matrixData);

            dsMatrixData.SaveJsonFile(listData);

            JsonDataSerializer dsLoadMatrixData = new JsonDataSerializer(JsonFileName);
            var loadMatrixDataJson = dsLoadMatrixData.LoadMatrixDataJson();
            Debug.Log("Name : " + loadMatrixDataJson.Count);
            Debug.Log("Loaded Data : " + loadMatrixDataJson);
            // Debug.Log(dataSerializer.LoadJsonFile("smallGraph"));

            //get wrong answers
            WrongAnswer answer = new WrongAnswer(graphSmall._allNodes.Count);
            Debug.Log(answer.GetWAnswer());

            answer.GetWAnswer().ForEach(elem =>
            {
                var txt = "";
                elem.ForEach(val => { txt += val + " ,"; });
                Debug.Log(txt);
            });

            //test for euler graph 

            List<List<int>> euler = new List<List<int>>();

            euler.Add(new List<int>() { 0, 1, 1, 1, 0 });
            euler.Add(new List<int>() { 1, 0, 1, 0, 0 });
            euler.Add(new List<int>() { 1, 1, 0, 0, 0 });
            euler.Add(new List<int>() { 1, 0, 0, 0, 1 });
            euler.Add(new List<int>() { 0, 0, 0, 1, 0 });

            EulerGraph eulerGraph = new EulerGraph("EulerGraph")
            {
                Matrix = euler
            };
            Debug.Log(eulerGraph.IsEulerian());
            Debug.Log(eulerGraph.IsEulerianText());
        }

        public void ProvideSmallInputs()
        {
            graphSmall = new Graph(Name);

            var a = graphSmall.CreateRoot("A");
            var b = graphSmall.CreateNode("B");
            var c = graphSmall.CreateNode("C");
            var d = graphSmall.CreateNode("D");
            var e = graphSmall.CreateNode("E");
            var f = graphSmall.CreateNode("E");

            a.AddEdge(b)
                .AddEdge(c);

            b.AddEdge(e)
                .AddEdge(d);
            d.AddEdge(e);
            e.AddEdge(f);

            //For the List Print
            Console.WriteLine("--------------List Print----------------");
            List<List<int>> res = graphSmall.CreateAdjacencyList();
            PrintMain.PrintList(res);
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
                matrixText.text += "  " + (char)('A' + i);
            }

            matrixText.text += "\r\n";

            for (int i = 0; i < Count; i++)
            {
                matrixText.text += " | [ " + (char)('A' + i);

                for (int j = 0; j < Count; j++)
                {
                    if (i == j)
                    {
                        matrixText.text += " &,";
                    }
                    else if (matrix[i, j] == null)
                    {
                        matrixText.text += " .,";
                    }
                    else
                    {
                        matrixText.text += " ," + matrix[i, j];
                    }
                }

                matrixText.text += " ]\r\n";
            }

            matrixText.text += "\r\n";
        }

        public void LoadMatrixDataInScene(Text txt)
        {
            Debug.Log("Button Clicked!");
            JsonDataSerializer dsLoadMatrixData = new JsonDataSerializer(JsonFileName);
            var loadMatrixDataJson = dsLoadMatrixData.LoadMatrixDataJson();

            foreach (var node in loadMatrixDataJson)
            {
                txt.text += node.Key + "\n";
                txt.text += Graph.GetStringValue(node.Value.nodes);
            }
            // txt.text = loadMatrixDataJson.nodeNames.ForEach();
            // loadMatrixDataJson.nodeNames.ForEach(elem =>
            // {
            //     txt.text += elem + " , ";
            // });
        }
    }
}