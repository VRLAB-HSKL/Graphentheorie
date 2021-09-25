using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Manager;
using UnityEngine;

namespace UndirectedGraph.Scripts.Subject
{
    public class Answer: VRKL.MBU.Subject
    {
        public JsonDataSerializer DataSerializer { get; set; }
        public List<List<int>> Nodes { get; set; }
        public List<MatrixData> NodeList { get; set; }
        public Dictionary<string, MatrixData> MatrixData { get; set; }
        
        public string Filename { get; set; }

        public Answer() : base()
        {
            DataSerializer = new JsonDataSerializer(Filename);
            if (DataSerializer != null) MatrixData = DataSerializer.LoadMatrixDataJson();
            NodeList = new List<MatrixData>();
            if (MatrixData != null)
                foreach (var elem in MatrixData)
                {
                    NodeList.Add(elem.Value);
                    Debug.Log(Graph.GetStringValue(elem.Value.nodes));
                }
        }

        public void Process()
        {
            MatrixData = DataSerializer.LoadMatrixDataJson();
            foreach (var elem in MatrixData)
            {
                NodeList.Add(elem.Value);
            }
            Notify();
        }
    }
}