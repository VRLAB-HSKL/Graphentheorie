using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AnswersManager : Observer
    {
        [SerializeField] private string filename;
        [SerializeField] private List<TextMeshPro> textFields;
        [SerializeField] private Material _material;
        private DataManagerSingleton _dataManager = DataManagerSingleton.Instance;
       
        /// <summary>
        /// The observed object
        /// </summary>
        protected Answer Model;


        private void Awake()
        {
            Model = new Answer() { Filename = filename, DataSerializer = new JsonDataSerializer(filename) };
            Model.Attach(this);
        }

        public void CreateAllRandomAnswers()
        {
            //JsonDataSerializer dataSerializer = new JsonDataSerializer("smallGraph");
            //var graphSmall = dataSerializer.LoadJsonFile("smallGraph");
            //get wrong answers
            //WrongAnswer answer = new WrongAnswer(Model.matrixData.Values.Count);
            //Debug.Log(answer.GetWAnswer());

            // foreach (var elem in Model.MatrixData)
            // {
            //     Model.NodeList.Add(elem.Value);
            // }

            textFields.ForEach(field =>
            {
                // answer.GetWAnswer().ForEach(elem =>
                // {
                //     var txt = "[ "; 
                //     elem.ForEach(val =>
                //     {
                //         txt += val + " ,";
                //     });
                //     field.text += txt + "\n";
                // });
                
                if (Model.NodeList.Any())
                {
                    var range = Random.Range(0, 3);
                    textFields[range].text = Graph.GetStringValue(Model.NodeList[range].nodes);
                }
            });
        }

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

        public Dictionary<AnswerType, string> BoardAnswers()
        {
            Dictionary<AnswerType, string> answers = new Dictionary<AnswerType, string>();
            textFields.ForEach(elem =>
            {
                answers.Add(GetAnswerType(elem.name),elem.text);
            });

            return answers;
        }
        
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
    }
}