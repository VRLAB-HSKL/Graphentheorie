using System.Collections.Generic;

namespace UndirectedGraph.Scripts.Subject
{
    /// <summary>
    /// EulerGraph is still a sub class of Graph and Provides extra Properties of the Euler into a Graph.
    /// </summary>
    public class EulerGraph : Graph
    {
        private List<List<int>> _matrix = null;
        private int[] _degrees;

        /// <summary>
        /// IsEulerian() method should be called first inorder to Calculate the EulerDegrees.
        /// </summary>
        public int[] Degrees
        {
            get => _degrees;
            set => _degrees = value;
        }

        public List<List<int>> Matrix
        {
            get => _matrix;
            set => _matrix = value;
        }

        /// <summary>
        /// Default Constructor 
        /// Mandatory : Call ConvertToMatrix() or initialize Matrix in order to function
        /// </summary>
        /// <param name="name"></param>
        public EulerGraph(string name) : base(name)
        {
        }

        /// <summary>
        /// Checks the Graph Matrix of the Attribute _matrix weather it has Euler Graph/Circuit
        /// 
        /// </summary>
        /// <returns>
        ///  returns 0 if Graph is not Eulerian
        /// returns 1 if Graph has an Eulerian Graph.
        /// returns 2 if The Graph has an Eulerian Circuit.
        /// </returns>
        public int IsEulerian()
        {
            _degrees = new int[_matrix.Count];
            int oddDegree = 0;
            if (!IsConnected())
            {
                return 0;
            }

            for (int i = 0; i < _matrix.Count; i++)
            {
                for (int j = 0; j < _matrix.Count; j++)
                {
                    if (_matrix[i][j] == 1)
                    {
                        _degrees[i]++;
                    }
                }

                if (_degrees[i] % 2 != 0)
                {
                    oddDegree++;
                }
            }

            if (oddDegree > 2)
            {
                return 0;
            }

            return oddDegree == 0 ? 2 : 1;
        }

        /// <summary>
        /// loops through each nodes of provided Graph Matrix and finds Visited Nodes in recursive way
        /// </summary>
        /// <param name="z">provided node index</param>
        /// <param name="visited">list of visited nodes</param>
        void Traverse(int z, bool[] visited)
        {
            visited[z] = true;
            for (int i = 0; i < _matrix.Count; i++)
            {
                if (_matrix[i][z] == 1)
                {
                    if (!visited[i])
                    {
                        Traverse(i, visited);
                    }
                }
            }
        }

        /// <summary>
        /// returns true if all nodes are visited
        /// first initializes the bool array as false
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            bool[] visited = new bool[_matrix.Count];
            for (int j = 0; j < _matrix.Count; j++)
            {
                for (int i = 0; i < _matrix.Count; i++)
                {
                    visited[i] = false;
                }

                Traverse(j, visited);

                for (int i = 0; i < _matrix.Count; i++)
                {
                    if (!visited[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// First provided Matrix should be converted to matrix inorder to function euler Graph checker
        /// </summary>
        public void ConvertToMatrix()
        {
            _matrix = CreateAdjacencyList();
        }

        /// <summary>
        /// returns the condition of Eulerian Graph in plain Text
        /// </summary>
        /// <returns></returns>
        public string IsEulerianText()
        {
            string result = "";
            switch (IsEulerian())
            {
                case 0:
                    result += "The Graph is not an Eulerian Graph.";
                    break;
                case 1:
                    result += "The Graph has an Eulerian Path.";
                    break;
                case 2:
                    result += "The Graph has an Eulerian Circuit.";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Implementation of HandShaking Concept of Euler Graph
        /// </summary>
        /// <returns>true if the sum of all corner degrees is equal to twice the number of edges</returns>
        public bool HandShaking()
        {
            if (_matrix == null)
            {
                return false;
            }

            IsEulerian();
            int sum = 0;
            for (int i = 0; i < Degrees.Length; i++)
            {
                sum += Degrees[i];
            }

            if ((2 * _matrix.Count) == sum)
            {
                return true;
            }

            return false;
        }
    }
}