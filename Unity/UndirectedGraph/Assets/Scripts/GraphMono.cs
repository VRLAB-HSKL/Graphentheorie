using System;
using UnityEngine;

namespace GraphContent
{
    public class GraphMono : MonoBehaviour
    {
        public Graph _Graph;
        public string Name;
        public string rootName;

        private void Start()
        {
            _Graph = new Graph(Name);
            _Graph.CreateRoot(rootName);
            
        }

        private void LateUpdate()
        {
            Debug.Log(_Graph.ToString());
        }
    }
}