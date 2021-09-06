using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;

namespace GraphGen
{
    public class AnswersManager : MonoBehaviour
    {
        [SerializeField] private List<TextMeshPro> textFields;
        [SerializeField] private Material _material;
        public void CreateAllAnswers()
        {
            JsonDataSerializer dataSerializer = new JsonDataSerializer("smallGraph");
            var graphSmall = dataSerializer.LoadJsonFile("smallGraph");
            //get wrong answers
            WrongAnswer answer = new WrongAnswer(graphSmall.Count);
            Debug.Log(answer.GetWAnswer());
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

                field.text = Graph.GetStringValue(answer.GetWAnswer());
            });

            textFields[Random.Range(0,3)].text = Graph.GetStringValue(graphSmall);
        }

        public void ChangeColor(GameObject board)
        {
            MeshRenderer meshRenderer = board.GetComponent<MeshRenderer>();
            meshRenderer.material = _material;
        }
    }
}