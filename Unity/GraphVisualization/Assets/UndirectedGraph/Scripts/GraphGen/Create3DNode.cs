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

        /// <summary>
        /// Creates a new Node with Gameobject as well Edges connected to each valid Node with Edge weight value 
        /// </summary>
        public void CreateNode()
        {
            JsonDataSerializer dataSerializer = new JsonDataSerializer("smallGraph");
            var data = dataSerializer.LoadJsonFile("smallGraph");

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