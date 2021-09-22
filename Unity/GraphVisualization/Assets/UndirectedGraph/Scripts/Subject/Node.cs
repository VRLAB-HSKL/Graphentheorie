using System.Collections.Generic;

namespace UndirectedGraph.Scripts.Subject
{
    public class Node
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public List<Edge> Edges = new List<Edge>();

        public Node(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Adds new child to the existing Node
        /// </summary>
        /// <param name="child">returns same node with added Child</param>
        /// <returns></returns>
        public Node AddEdge(Node child)
        {
            Edges.Add(new Edge
            {
                Parent = this,
                Child = child
            });

            if (!child.Edges.Exists(val => val.Parent == child && val.Child == this))
            {
                child.AddEdge(this);
            }

            return this;
        }

        /// <summary>
        /// Checks either the Node has connection with the parameter node n
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool HasConnection(Node n)
        {
            if (Edges.Exists(val => val.Parent == this && val.Child == n))
            {
                return true;
            }

            return false;
        }
    }
}