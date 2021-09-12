using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;

namespace GraphGen
{
    public class Create3DNode : MonoBehaviour
    {
        [SerializeField] private GameObject NodePrefab;
        [SerializeField] private GameObject EdgePrefab;

        [SerializeField] private Material _material;
        [SerializeField] private string fileName;
        [SerializeField] private string jsonBlockName;
        [SerializeField] private Material startingPointMat;

        /// <summary>
        /// Creates a new Node with Gameobject as well Edges connected to each valid Node with Edge weight value 
        /// </summary>
        public void CreateNode()
        {
            JsonDataSerializer dataSerializer = new JsonDataSerializer(fileName);
            var data = dataSerializer.LoadJsonFile(fileName);

            var prefabs = InstantiateNode(data.Count);

            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data.Count; j++)
                {
                    if (data[i][j] == 1)
                    {
                        DrawEdgeLine(prefabs[i].transform.position, prefabs[j].transform.position,
                            new Color(150, 150, 150));
                    }
                }
            }
        }

        public void CreateGraphFromData()
        {
            JsonDataSerializer dataSerializer = new JsonDataSerializer(fileName);
            var matrixDict = dataSerializer.LoadMatrixDataJson();
            var data = new List<List<int>>();
            List<System.Numerics.Vector3> pos = null;
            List<string> names = null;
            string startingPoint = null;

            foreach (var graph in matrixDict)
            {
                if (graph.Key.Contains(jsonBlockName))
                {
                    data = graph.Value.nodes;
                    pos = graph.Value.nodePositions;
                    names = graph.Value.nodeNames;
                    startingPoint = graph.Value.startingPoint;
                }
            }
            
            // var prefabs = InstantiateNode(data.Count);
            var prefabs = InstantiateNode(pos,names);
            //change start node color
            StartingPoint(startingPoint,prefabs);
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < data.Count; j++)
                {
                    if (data[i][j] == 1)
                    {
                        DrawEdgeLine(prefabs[i].transform.position, prefabs[j].transform.position,
                            new Color(150, 150, 150));
                    }
                }
            }
            
        }
        /// <summary>
        /// Instantiates the provided Node Prefab and saves it as a Gameobject in a List
        /// </summary>
        /// <param name="data">Number of total Nodes</param>
        /// <returns></returns>

        private List<GameObject> InstantiateNode(int index)
        {
            List<GameObject> prefabs = new List<GameObject>();
            float radius = 5f;
            int nameVal = 65;
            for (int i = 0; i < index; i++)
            {
                float angle = i * Mathf.PI * 2f / 8;
                Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, 5f, Mathf.Sin(angle) * radius);
                GameObject pfab = Instantiate(NodePrefab, newPos, Quaternion.identity);
                var name = System.Convert.ToChar(nameVal + i).ToString();
                pfab.name = name;
                pfab.GetComponentInChildren<TextMesh>().text = name;
                prefabs.Add(pfab);
            }

            return prefabs;
        }

        private List<GameObject> InstantiateNode(List<System.Numerics.Vector3> positions, List<string> names)
        {
            var prefabs = new List<GameObject>();

            for (int i = 0; i < positions.Count; i++)
            {
                Vector3 newPos = new Vector3(positions[i].X,positions[i].Y+5f,positions[i].Z);
                GameObject pfab = Instantiate(NodePrefab, newPos, Quaternion.identity);
                if (names.Count != positions.Count)
                {
                    var remain = positions.Count - names.Count;
                    int nameVal = 65;
                    for (int j = 0; j < remain; j++)
                    {
                        names.Add( System.Convert.ToChar(nameVal++).ToString());
                    }
                }
                pfab.name = names[i];
                pfab.GetComponentInChildren<TextMesh>().text = names[i];
                prefabs.Add(pfab);
            }

            return prefabs;
        }

        public void StartingPoint(string name, List<GameObject> prefabs)
        {
            prefabs.ForEach(elem =>
            {
                if (elem.name.Contains(name))
                {
                    elem.GetComponent<Renderer>().material = startingPointMat;
                }
            });
        }
        /// <summary>
        /// Creates new Line for the connected Edge
        /// </summary>
        /// <param name="start">Start Edge point</param>
        /// <param name="end">End Edge Point</param>
        /// <param name="color"></param>
        /// <param name="duration"></param>
        public void DrawEdgeLine(Vector3 start, Vector3 end, Color color, string name = "Edge")
        {
            GameObject edge = new GameObject();
            edge.transform.position = start;
            edge.AddComponent<LineRenderer>();
            LineRenderer lr = edge.GetComponent<LineRenderer>();
            edge.name = name;
            lr.material = _material;
            lr.SetColors(color, color);
            lr.SetWidth(0.1f, 0.1f);
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
        }
    }
}