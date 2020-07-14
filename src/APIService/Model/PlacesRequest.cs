using DomainEntities.PlaceSearch;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace APIService.Model
{
    public class PlacesRequest
    {
        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "componentContries", NullValueHandling = NullValueHandling.Ignore)]
        public ComponentCountries ComponentContries { get; set; }

        [JsonProperty(PropertyName = "offset", NullValueHandling = NullValueHandling.Ignore)]
        public int? Offset { get; set; }

        [JsonProperty(PropertyName = "originPoint", NullValueHandling = NullValueHandling.Ignore)]
        public GeographicalCoordinates OriginPoint { get; set; }

        [JsonProperty(PropertyName = "language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "location", NullValueHandling = NullValueHandling.Ignore)]
        public GeographicalCoordinates Location { get; set; }

        [JsonProperty(PropertyName = "radius", NullValueHandling = NullValueHandling.Ignore)]
        public int? Radius { get; set; }

        [JsonProperty(PropertyName = "sessiontoken", NullValueHandling = NullValueHandling.Ignore)]
        public string SessionToken { get; set; }

        [JsonProperty(PropertyName = "strictbounds", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Strictbounds { get; set; }

        [JsonProperty(PropertyName = "types", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeOfResults Types { get; set; }


        [JsonProperty(PropertyName = "apiKey", NullValueHandling = NullValueHandling.Ignore)]
        public string ApiKey { get; set; }


        [JsonProperty(PropertyName = "operationId", NullValueHandling = NullValueHandling.Ignore)]
        public string OperationId { get; set; }

        [JsonProperty(PropertyName = "cancellationToken", NullValueHandling = NullValueHandling.Ignore)]
        public CancellationToken CancellationToken { get; set; }
    }
}