using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
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
            TextAsset json = Resources.Load("json/"+fileName, typeof(TextAsset)) as TextAsset;
            // Debug.Log(json);
            List<List<int>> templete = new List<List<int>>();
            if (json is { })
            {
                templete = JsonConvert.DeserializeObject<List<List<int>>>(json.text);
            }
            return templete;
        }
    }
}