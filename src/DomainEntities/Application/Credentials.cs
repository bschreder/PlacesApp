using Newtonsoft.Json;

namespace DomainEntities.Application
{
    public class Credentials
    {
        [JsonProperty(PropertyName = "placesApiKey", NullValueHandling = NullValueHandling.Ignore)]
        public string PlacesApiKey { get; private set; }

        [JsonProperty(PropertyName = "authorizeKey", NullValueHandling = NullValueHandling.Ignore)]
        public string AuthorizeKey { get; private set; }
    }
}