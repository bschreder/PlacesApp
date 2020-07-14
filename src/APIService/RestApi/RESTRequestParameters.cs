using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;

namespace APIService.RestApi
{
    public class RESTRequestParameters
    {
        [JsonProperty(PropertyName = "headers", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Headers { get; set; }

        [JsonProperty(PropertyName = "url", Required = Required.Always)]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "cancellationToken", NullValueHandling = NullValueHandling.Ignore)]
        public CancellationToken CancellationToken { get; set; }
    }
}
