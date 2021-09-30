using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GraphContent;
using GraphGen;
using Manager;
using TMPro;
using UndirectedGraph.Scripts.Subject;
using UnityEngine;
using UnityEngine.Serialization;
using VRKL.MBU;
using Random = UnityEngine.Random;

namespace UndirectedGraph.Scripts.UI
{
    /// <summary>
    /// An Observer class which provides which should be attached to a Gameobject , in order to load the matrix data in provided Text fields. 
    /// </summary>
    public class AnswersManager : Observer
    {
        [SerializeField] private List<TextMeshPro> textFields;
        [SerializeField] private Material _material;
        private DataManagerSingleton _dataManager = DataManagerSingleton.Instance;

        /// <summary>
        /// The observed object
        /// </summary>
        protected Answer Model;


        private void Start()
        {
            if (_dataManager.FileName == null)
            {
                Thread.Sleep(5);
            }

            Model = new Answer()
                { Filename = _dataManager.FileName, DataSerializer = new JsonDataSerializer(_dataManager.FileName) };
            Model.Attach(this);
        }

        /// <summary>
        /// Loads all the Node block from JSON and distributes it to all 3 Boards.
        /// </summary>
        public void CreateAllRandomAnswers()
        {
            if (_dataManager.FileName == null)
            {
                Thread.Sleep(505);
                textFields.ForEach(field =>
                {
                    if (Model.NodeList.Any())
                    {
                        var range = Random.Range(0, 3);
                        textFields[range].text = Graph.GetStringValue(Model.NodeList[range].nodes);
                    }
                });
            }
            else
            {
                textFields.ForEach(field =>
                {
                    if (Model.NodeList.Any())
                    {
                        var range = Random.Range(0, 3);
                        textFields[range].text = Graph.GetStringValue(Model.NodeList[range].nodes);
                    }
                });
            }
        }

        /// <summary>
        /// Changes material of provided GameObject
        /// </summary>
        /// <param name="board"></param>
        public void ChangeColor(GameObject board)
        {
            MeshRenderer meshRenderer = board.GetComponent<MeshRenderer>();
            meshRenderer.material = _material;
        }

        #region Overrides of Observer

        public override void Refresh()
        {
            CreateAllRandomAnswers();
        }

        #endregion

        private void FixedUpdate()
        {
            Model.Process();
        }

        /// <summary>
        /// loads the textfields text and returns a Dictionary with key
        /// </summary>
        /// <returns></returns>
        public Dictionary<AnswerType, string> BoardAnswers()
        {
            Dictionary<AnswerType, string> answers = new Dictionary<AnswerType, string>();
            textFields.ForEach(elem => { answers.Add(GetAnswerType(elem.name), elem.text); });

            return answers;
        }

        /// <summary>
        /// returns the AnswerType from the String provided from unity editor
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AnswerType GetAnswerType(string value)
        {
            switch (value)
            {
                case "A":
                    return AnswerType.AnswerA;
                    break;
                case "B":
                    return AnswerType.AnswerB;
                    break;
                case "C":
                    return AnswerType.AnswerC;
                    break;
                default:
                    return AnswerType.None;
            }
        }

        public static string GetAnswerType(AnswerType value)
        {
            switch (value)
            {
                case AnswerType.AnswerA:
                    return "A";
                    break;
                case AnswerType.AnswerB:
                    return "B";
                    break;
                case AnswerType.AnswerC:
                    return "C";
                    break;
                default:
                    return "";
            }
        }

        public bool CheckEulerPath()
        {
            var eulerGraph = EulerGraphFunc();

            if (eulerGraph.IsEulerian() == 1)
            {
                return true;
            }

            return false;
        }

        public bool CheckEulerCircuit()
        {
            var eulerGraph = EulerGraphFunc();

            if (eulerGraph.IsEulerian() == 2)
            {
                return true;
            }

            return false;
        }

        public bool HandShaking()
        {
            var eulerGraph = EulerGraphFunc();

            if (eulerGraph.HandShaking())
            {
                return true;
            }

            return false;
        }

        private GraphAdvanced EulerGraphFunc()
        {
            var eulerGraph = new GraphAdvanced("EulerGraph")
            {
                Matrix = Model.MatrixData[GetAnswerType(_dataManager.RightAnswer)].nodes
            };
            return eulerGraph;
        }
    }
}