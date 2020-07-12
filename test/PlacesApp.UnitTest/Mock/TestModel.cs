using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesApp.UnitTest.Mock
{
    public class TestModel
    {
        [JsonProperty(PropertyName = "intResultint", Required = Required.Always)]
        public int IntResult { get; set; }
    }
}
