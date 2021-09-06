using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphContent;

public class Graph
{
    public Node Root;
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
        Root = CreateNode(name);
        return Root;
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

    public static string GetStringValue(List<List<int>> data)
    {
        string res = "";
        data.ForEach(elem =>
        {
            res += "[ ";
            elem.ForEach(val =>
            {
                res += val + " ,";
            });

            res += " ]\n";
        });
        return res;
    }
    
    public override string ToString()
    {
        return "Name " + _name + "AllNodes " + _allNodes.ToString();
    }
}