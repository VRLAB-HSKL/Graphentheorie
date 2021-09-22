using System.Collections.Generic;
using GraphContent;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace UndirectedGraph.Scripts.Subject
{
    /// <summary>
    /// Properties and Functionalities for the class MatrixData
    /// </summary>
    public class MatrixDataManager
    {
        private MatrixData _matrixData;

        /// <summary>
        /// AT first a MatrixData is expected where information about the Graph are saved. 
        /// </summary>
        /// <param name="matrixData"></param>
        public MatrixDataManager(MatrixData matrixData)
        {
            _matrixData = matrixData;
        }

        public float GetAngle(string name)
        {
            var value = 0;

            for (int i = 0; i < _matrixData.nodeNames.Count; i++)
            {
                if (_matrixData.nodeNames[i].Contains(name))
                {
                    value = i;
                }
            }

            return GetAngle(value);
        }

        public float GetAngle(int index)
        {
            var connections = new List<Vector3>();
            var nodes = _matrixData.nodes[index];
            if (nodes.Count < index)
            {
                return 0.0f;
            }
            Graph g = new Graph(_matrixData.name).CreateGraph(_matrixData);
            
            Debug.Log(Graph.GetStringValue(g.CreateAdjacencyList()));

            Node current = g.AllNodes[index];

            if (current.Edges.Count >= 2)
            {
                //calculate angle
            }
            return 0.0f;
        }

        public Dictionary<int, List<int>> Connections()
        {
            Dictionary<int,List<int>> res = new Dictionary<int, List<int>>();

            for (int i = 0; i < _matrixData.nodes.Count; i++)
            {
                List<int> connectList = new List<int>();
                for (int j = 0; j < _matrixData.nodes.Count; j++)
                {
                    if (true)
                    {
                        if (_matrixData.nodes[i][j]  == 1)
                        {
                            connectList.Add(j);
                        }
                    }
                }
                res.Add(i,connectList);
            }
            return res;
        }
    }
}