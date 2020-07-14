using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace DomainEntities.PlaceSearch
{
    public class ComponentCountries
    {
        [JsonProperty(PropertyName = "components", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Components { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            
            for (int i = 0; i < Components.Count; i++)
            {
                if (i == 0)
                    builder.Append($"country:{Components[i].Substring(0, 2)}");
                else
                    builder.Append($"|country:{Components[i].Substring(0, 2)}");
            }

            return $"components={builder}";
        }

        public KeyValuePair<string, string> ToKeyValuePair() =>
             new KeyValuePair<string, string>("components", string.Join("|", $"country:{Components}"));
    }
}
