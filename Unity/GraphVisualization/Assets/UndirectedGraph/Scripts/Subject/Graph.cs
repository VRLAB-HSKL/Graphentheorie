using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphContent;
using UndirectedGraph.Scripts.Subject;

public class Graph
{
    public Node _root;
    private string _name;
    public List<Node> _allNodes = new List<Node>();


    /// <summary>
    /// 
    /// </summary>
    public Graph(string name)
    {
        _name = name;
    }

    /// <summary>
    /// A new Node will be created and added to the total Node Container
    /// @param: name : Name of the new Node which will be created
    /// </summary>
    public Node CreateNode(string name)
    {
        var nodeTemp = new Node(name);
        _allNodes.Add(nodeTemp);
        return nodeTemp;
    }

    /// <summary>
    /// Creates Root for the Whole Graph with the provided name from param : name
    /// </summary>
    public Node CreateRoot(string name)
    {
        _root = CreateNode(name);
        return _root;
    }

    /// <summary>
    /// Creates a matrix and figures out if the Nodes have connection, in case of connection 1 will be added to the matrix.
    /// </summary>
    public int?[,] CreateAdjacencyMatrix()
    {
        int?[,] adj = new int?[_allNodes.Count, _allNodes.Count];

        for (int i = 0; i < _allNodes.Count; i++)
        {
            Node n1 = _allNodes[i];

            for (int j = 0; j < _allNodes.Count; j++)
            {
                Node n2 = _allNodes[j];
                var edge = n1.Edges.FirstOrDefault(a => a.Child == n2);

                if (edge != null)
                {
                    adj[i, j] = 1;
                }
                else
                {
                    adj[i, j] = 0;
                }
            }
        }

        return adj;
    }

    /// <summary>
    /// Creates Adjacency List from the Nodes and Root
    /// </summary>
    /// <returns>List of int values</returns>
    public List<List<int>> CreateAdjacencyList()
    {
        List<List<int>> result = new List<List<int>>();

        for (int i = 0; i < _allNodes.Count; i++)
        {
            List<int> temp = new List<int>();
            Node n1 = _allNodes[i];
            for (int j = 0; j < _allNodes.Count; j++)
            {
                Node n2 = _allNodes[j];
                var edge = n1.Edges.FirstOrDefault(a => a.Child == n2);

                if (edge != null)
                {
                    temp.Add(1);
                }
                else
                {
                    temp.Add(0);
                }
            }

            result.Add(temp);
        }

        return result;
    }

    /// <summary>
    /// provided list of data will be saved as a string and returned.
    /// Usage : showing text data in view
    /// </summary>
    /// <param name="data">List of Nodes with their values in it</param>
    /// <returns></returns>
    public static string GetStringValue(List<List<int>> data)
    {
        string res = "";
        data.ForEach(elem =>
        {
            res += "[ ";
            elem.ForEach(val => { res += val + " ,"; });

            res += " ]\n";
        });
        return res;
    }

    /// <summary>
    /// Getters and Setters / Properties
    /// </summary>
    public Node Root
    {
        get => _root;
        set => _root = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public List<Node> AllNodes
    {
        get => _allNodes;
        set => _allNodes = value;
    }

    /// <summary>
    /// returns Total Nos of Nodes
    /// </summary>
    /// <returns></returns>
    public int NodeSize()
    {
        return _allNodes.Count;
    }

    /// <summary>
    /// counts one edge if both node has connection with each other
    /// </summary>
    /// <returns>total number of Edges connected to different Nodes</returns>
    public int TotalEdges()
    {
        var matrix = CreateAdjacencyList();
        var size = 0;
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix[i].Count; j++)
            {
                if (matrix[i][j] == matrix[j][i])
                {
                    size++;
                }
            }
        }

        return size;
    }

    /// <summary>
    /// JSON Graph Data to Graph Nodes and Edges
    /// Note : Not Functional 
    /// </summary>
    /// <returns></returns>
    ///
    public Graph CreateGraph(MatrixData matrixData)
    {
        CreateRoot(matrixData.startingPoint);
        List<Node> nodesGen = new List<Node>(); 
        
        CreateNodesFromNames();
        CreateEdges();
        
        void CreateEdges()
        {
            for (int i = 0; i < matrixData.nodes.Count; i++)
            {
                for (int j = 0; j < matrixData.nodes[i].Count; j++)
                {
                    if (matrixData.nodes[i][j] == matrixData.nodes[j][i])
                    {
                        if (!matrixData.nodeNames[j].Contains(matrixData.nodeNames[i]))
                        {
                            nodesGen[i].AddEdge(nodesGen[j]);
                        }
                    }
                }
            }
        }
        
        void CreateNodesFromNames()
        {
            var nameVal = 65;
            for (int i = 0; i < matrixData.nodes.Count; i++)
            {
                if (matrixData.nodeNames[i] != null)
                {
                    nodesGen.Add(CreateNode(matrixData.nodeNames[i]));
                }
                else
                {
                    nodesGen.Add(CreateNode(System.Convert.ToChar(nameVal++).ToString()));
                }
            }
        }

        return this;
    }

    public override string ToString()
    {
        return $"{nameof(_root)}: {_root}, {nameof(_name)}: {_name}, {nameof(_allNodes)}: {_allNodes}";
    }
}