using System;
using System.Collections.Generic;
using GraphContent;
using TMPro;
using UndirectedGraph.Scripts.Subject;
using UnityEngine;
using UnityEngine.UI;

namespace UndirectedGraph.Scripts.UI
{
    /// <summary>
    /// Checks the Answer of the 3D Graph plotted after one Answer button is clicked. 
    /// This class should be attached to a Gameobject and provide a Panel and TextMeshPro as input.
    /// After the previous Process the Parent Gameobject should be attached in the OnClick() function of the
    /// Answer Button
    /// </summary>
    public class AnswerChecker : MonoBehaviour
    {
        [TextArea] [Tooltip("How does it work?")] [SerializeField]
        private string Notes = "";

        [Tooltip(
            "Attach this gameobject to a button OnClick() and select the method Check. A,B or C should be written in the Textfield which is the name of the answer user selects.")]
        [SerializeField]
        private AnswersManager manager;

        [Tooltip("A Panel where the Message should be Displayed.")] [SerializeField]
        private GameObject panel;

        [Tooltip("A Text Area where the Message Text should be Displayed.")] [SerializeField]
        private TextMeshPro answerCheckDisplay;

        [Tooltip("A Default Camera following Gameobject to show the Dialog Panel.")] [SerializeField]
        private GameObject panelPlaceholder;

        [Header("Materials for Success and Fail")] [SerializeField]
        private Material success;

        [SerializeField] private Material fail;

        [Header("Lights for each Board")]
        [SerializeField]
        public List<GameObject> lights;

        private DataManagerSingleton _dataManager = DataManagerSingleton.Instance;

        private bool state;


        private void Awake()
        {
            panel.SetActive(false);
        }

        /// <summary>
        /// Checks answer after "Answer" button is clicked
        /// </summary>
        /// <param name="answerGo"></param>
        public void Check(string answerName)
        {
            panel.SetActive(true);
            state = true;

            if (GetResult(AnswersManager.GetAnswerType(answerName)))
            {
                answerCheckDisplay.text = "Selected Answer is right! \n You selected " + answerName;
                panel.GetComponent<Image>().material = success;
                //lights[answerName].color = new LinearColor()
                //lights[GetIndex(answerName)].color = new LinearColor();
                lights[GetIndex(answerName)].GetComponent<Light>().color = Color.green;
            }
            else
            {
                answerCheckDisplay.text = "Selected Answer is wrong! \n You selected " + answerName;
                panel.GetComponent<Image>().material = fail;
                lights[GetIndex(answerName)].GetComponent<Light>().color = Color.red;
            }
        }

        private int GetIndex(string answerName)
        {
            if (AnswersManager.GetAnswerType(answerName) == AnswerType.AnswerA)
            {
                return 0;
            }

            if (AnswersManager.GetAnswerType(answerName) == AnswerType.AnswerB)
            {
                return 1;
            }

            if (AnswersManager.GetAnswerType(answerName) == AnswerType.AnswerC)
            {
                return 2;
            }

            throw new InvalidOperationException();
        }

        private void Update()
        {
            if (state)
            {
                var transform1 = panelPlaceholder.transform;
                // panel.transform.position = transform1.position + new Vector3(10, 0, 10);
                //panel.transform.rotation = transform1.rotation;

                // Vector3 relativePos = transform1.position - panel.transform.position;

                // the second argument, upwards, defaults to Vector3.up
                //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                //transform1.rotation = rotation;

                // vrCamera.transform.LookAt(panel.transform);
                // vrCamera.transform.rotation = Quaternion.FromToRotation(Vector3.left, Vector3.forward);


                // the second argument, upwards, defaults to Vector3.up
                // Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0,1,0));
                // vrCamera.transform.rotation = rotation*Quaternion.Euler(0,90,0);   

                //vrCamera.transform.LookAt(panel.transform);

                panel.transform.position = panelPlaceholder.transform.position;
                panel.transform.rotation = panelPlaceholder.transform.rotation;
            }
        }


        private bool GetResult(AnswerType selectedAnswer)
        {
            manager.BoardAnswers();
            if (_dataManager.RightAnswer == selectedAnswer)
            {
                return true;
            }

            return false;
        }

        public void Reset()
        {
            state = false;
            panel.SetActive(false);
            lights.ForEach(elem =>
            {
                elem.GetComponent<Light>().color = Color.blue;
            });
        }

        public void EulerPath(GameObject go)
        {
            if (manager.CheckEulerPath())
            {
                Debug.Log("has Euler Path");
                go.GetComponent<Image>().color = Color.green;
            }
            else
            {
                Debug.Log("Does not have Euler Path");
                go.GetComponent<Image>().color = Color.red;
            }

        }

        public void EulerCircuit(GameObject go)
        {
            if (manager.CheckEulerCircuit())
            {
                Debug.Log("has Euler Circuit");
                go.GetComponent<Image>().color = Color.green;
            }
            else
            {
                Debug.Log("Does not have Euler Circuit");
                go.GetComponent<Image>().color = Color.red;
            }
        }
        
        public void HandShaking(GameObject go)
        {
            if (manager.CheckEulerCircuit())
            {
                Debug.Log("has Euler Circuit");
                go.GetComponent<Image>().color = Color.green;
            }
            else
            {
                Debug.Log("Does not have Euler Circuit");
                go.GetComponent<Image>().color = Color.red;
            }
        }
    }
}