using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DomainEntities.PlaceSearch
{
    public enum TypeOfResults
    {
        [Description("")]
        None,

        [Description("geocode")]
        Geocode,

        [Description("address")]
        Address,

        [Description("establishment")]
        Establishment,

        [Description("(regions)")]
        Regions,

        [Description("(cities)")]
        Cities
    }
}
