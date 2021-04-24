using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Engine
{
    public class JsonParser
    {
        public static List<T> ExtractListData<T>(string r)
        {
            List<T> lt = JsonConvert.DeserializeObject <List<T>>(r);
            return lt;
        }

        public static T ExtractSingleData<T>(string r, string name)
        {
            JObject obj = JObject.Parse(r);
            string objJson = obj[name].ToString();

            T t = JsonConvert.DeserializeObject<T>(objJson);
            return t;
        }
    }
}
