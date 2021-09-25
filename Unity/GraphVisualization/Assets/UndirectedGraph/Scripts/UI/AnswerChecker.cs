using TMPro;
using UnityEngine;

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
        [SerializeField] private GameObject panel;
        [SerializeField] private TextMeshPro answerCheckDisplay;

        private void Awake()
        {
            panel.SetActive(false);
        }

        /// <summary>
        /// Checks answer after "Answer" button is clicked
        /// </summary>
        /// <param name="answerGo"></param>
        public void Check(GameObject answerGo)
        {
            panel.SetActive(true);
            answerCheckDisplay.text = "Clicked Answer :- " + answerGo.GetComponent<TextMeshPro>().name;
        }
    }
}