using System.Collections.Generic;
using System.IO;
using GraphContent;
using Newtonsoft.Json;
using UndirectedGraph.Scripts.Subject;
using UnityEngine;

namespace Manager
{
    public class JsonDataSerializer
    {
        private string FileName;

        /// <summary>
        /// Constructor parameter expects a Filename which should be loaded or saved as.
        /// </summary>
        /// <param name="fileName"></param>
        public JsonDataSerializer(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Saves Json Data in a File in the Assets/Resources/json folder
        /// Saves only the Nodes Data
        /// </summary>
        /// <param name="nodes"></param>
        public void SaveJsonFile(List<List<int>> nodes)
        {
            var filePath = Application.dataPath + "/Resources/json/" + FileName + ".json";
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(@"" + filePath, JsonConvert.SerializeObject(nodes));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(@"" + filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, nodes);
            }
        }

        /// <summary>
        /// Loads json File saved in the folder Resources.
        /// </summary>
        public List<List<int>> LoadJsonFile(string fileName)
        {
            TextAsset json = Resources.Load("json/" + fileName, typeof(TextAsset)) as TextAsset;
            // Debug.Log(json);
            List<List<int>> templete = new List<List<int>>();
            if (json is { })
            {
                templete = JsonConvert.DeserializeObject<List<List<int>>>(json.text);
            }

            return templete;
        }

        /// <summary>
        /// Saves provided MatrixData class Attribute details in provided FileName
        /// Saves Json Data in a File in the Assets/Resources/json folder
        /// </summary>
        /// <param name="data"></param>
        public void SaveJsonFile(Dictionary<string, MatrixData> data)
        {
            var filePath = Application.dataPath + "/Resources/json/" + FileName + ".json";
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(@"" + filePath, JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(@"" + filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, data);
            }
        }

        /// <summary>
        /// Loads Data saved in a JSON Data from provided file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public Dictionary<string, MatrixData> LoadMatrixDataJson(string fileName)
        {
            TextAsset json = Resources.Load("json/" + fileName, typeof(TextAsset)) as TextAsset;
            // Debug.Log(json);
            Dictionary<string, MatrixData> templete = new Dictionary<string, MatrixData>();
            if (json is { })
            {
                templete = JsonConvert.DeserializeObject<Dictionary<string, MatrixData>>(json.text,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
            }

            return templete;
        }

        public Dictionary<string, MatrixData> LoadMatrixDataJson()
        {
            return LoadMatrixDataJson(FileName);
        }
    }
}