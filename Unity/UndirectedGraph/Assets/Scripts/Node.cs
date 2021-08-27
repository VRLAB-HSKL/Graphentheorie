using System.Collections.Generic;

namespace GraphContent
{
    public class Node
    {
        public string Name;
        public List<Edge> Edges = new List<Edge>();
        public Node(string name)
        {
            Name = name;
        }

        public Node AddEdge(Node child)
        {
            Edges.Add(new Edge
            {
                Parent =  this,
                Child = child
            });

            if (!child.Edges.Exists(val => val.Parent == child && val.Child == this))
            {
                child.AddEdge(this);
            }

            return this;
        }
    }
}