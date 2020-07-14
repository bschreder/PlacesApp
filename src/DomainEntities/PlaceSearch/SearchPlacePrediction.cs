using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainEntities.PlaceSearch
{
    public class SearchPlacePrediction
    {
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "distanceMeters", NullValueHandling = NullValueHandling.Ignore)]
        public int DistanceMeters { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "placeId", NullValueHandling = NullValueHandling.Ignore)]
        public string PlaceId { get; set; }

        [JsonProperty(PropertyName = "reference", NullValueHandling = NullValueHandling.Ignore)]
        public string Reference { get; set; }

        [JsonProperty(PropertyName = "types", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Types { get; set; }

    }
}
