using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DomainEntities.PlaceSearch
{
    public class SearchPlacesResponse
    {
        [JsonProperty(PropertyName = "predictions", NullValueHandling = NullValueHandling.Ignore)]
        public List<SearchPlacePrediction> Predictions { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }


        //  service parameters
        [JsonProperty(PropertyName = "sessiontoken", NullValueHandling = NullValueHandling.Ignore)]
        public string SessionToken { get; set; }

        [JsonProperty(PropertyName = "displayErrors", NullValueHandling = NullValueHandling.Ignore)]
        public bool? DisplayErrors { get; set; }

        [JsonProperty(PropertyName = "operationId", NullValueHandling = NullValueHandling.Ignore)]
        public string OperationId { get; set; }
    }
}