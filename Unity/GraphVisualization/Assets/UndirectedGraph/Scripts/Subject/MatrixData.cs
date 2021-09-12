using System.Collections.Generic;
using System.Numerics;

namespace GraphContent
{
    public class MatrixData
    {
        /// <summary>
        /// Node Names should be in an Order as the Nodes.
        /// For Instance A,B,C are the Names and
        /// A has the 1st Value of nodes List , B 2nd and C the 3rd.
        /// </summary>
        public List<List<int>> nodes;
        public string name;
        public string startingPoint;
        public List<string> nodeNames;
        public List<Vector3> nodePositions = new List<Vector3>();
    }
}