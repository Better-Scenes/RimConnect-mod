using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimConnection
{
    public class NewtonsoftRestSerializer : IRestSerializer
    {
        public string[] SupportedContentTypes => new string[]{
            "application/json", "text/json"
        };

        public DataFormat DataFormat => DataFormat.Json;

        public string ContentType { get; set; } = "application/json";

        public T Deserialize<T>(IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public string Serialize(Parameter parameter)
        {
            return JsonConvert.SerializeObject(parameter.Value);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
