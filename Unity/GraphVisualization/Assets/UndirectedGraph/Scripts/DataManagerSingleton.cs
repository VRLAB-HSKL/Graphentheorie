using System;
using System.IO;
using Newtonsoft.Json;
using Subject;
using UndirectedGraph.Scripts.Subject;
using UnityEngine;

namespace GraphContent
{
    ///
    ///Singleton Class which Saves the Data
    ///
    public class DataManagerSingleton
    {
        private static DataManagerSingleton _instance = null;
        private static readonly object Padlock = new object();

        public AnswerType RightAnswer { get; set; } = AnswerType.None;
        
        public string FileName { get; set; }


        public bool MenuState { get; set; } = false;

        private DataManagerSingleton()
        {
        }

        public static DataManagerSingleton Instance
        {
            get
            {
                lock (Padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new DataManagerSingleton();
                    }

                    return _instance;
                }
            }
        }

        public Matrix ParseInFile()
        {
            TextAsset json = Resources.Load("json/template", typeof(TextAsset)) as TextAsset;
            // Debug.Log(json);
            Matrix templete = new Matrix();
            if (json is { })
            {
                templete = JsonConvert.DeserializeObject<Matrix>(json.text);
            }

            return templete;
        }

        public string SaveJsonData(Matrix mat)
        {
            var res = JsonConvert.SerializeObject(mat);
            return res;
        }

        /// <summary>
        /// Saves provided Matrix in matrix.json
        /// Test Method, Not implemented in main Program
        /// </summary>
        /// <param name="mat"></param>
        public void SaveJsonFile(Matrix mat)
        {
            var filePath = Application.dataPath + "/Resources/json/matrix.json";
            // serialize JSON to a string and then write string to a file
            File.WriteAllText(@""+filePath, JsonConvert.SerializeObject(mat));

            // serialize JSON directly to a file
            using (StreamWriter file = File.CreateText(@""+filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, mat);
            }
        }
    }
}