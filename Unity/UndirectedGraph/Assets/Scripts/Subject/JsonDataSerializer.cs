using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Subject
{
    public class JsonDataSerializer
    {
        private JsonSerializer _serializer = new JsonSerializer();


        public JsonDataSerializer()
        {
            _serializer.Converters.Add(new JavaScriptDateTimeConverter());
            _serializer.NullValueHandling = NullValueHandling.Ignore;

            Graph g = new Graph("Json Test");
            
            using (StreamWriter sw = new StreamWriter(@"C:\json.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                {
                    _serializer.Serialize(writer, g);
                }
            }
        }

    }
}