using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ArdalisRating.Serialization
{
    public class PolicySerializer
    {
        public Policy GetPolicyFromJsonString(string json)
        {
            return JsonConvert.DeserializeObject<Policy>(json, new StringEnumConverter());
        }
    }
}
