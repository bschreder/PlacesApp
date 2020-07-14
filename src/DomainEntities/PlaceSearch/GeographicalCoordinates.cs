using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainEntities.PlaceSearch
{
    public class GeographicalCoordinates
    {
        [JsonProperty(PropertyName = "latitude", NullValueHandling = NullValueHandling.Ignore)]
        public string Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude", NullValueHandling = NullValueHandling.Ignore)]
        public string Longitude { get; set; }

        public override string ToString() => 
            $"location={Longitude},{Latitude}";

        public KeyValuePair<string, string> ToKeyValuePair() =>
            new KeyValuePair<string, string>("location", $"{Longitude},{Latitude}");
    }
}
