
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace be4care.Helpers
{
    internal class JsonHelper 
    {
        public JsonHelper()
        {
        }

        public string RootElement { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Namespace { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string DateFormat { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public static T Deserialize<T>(IRestResponse response)
        {
             return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
