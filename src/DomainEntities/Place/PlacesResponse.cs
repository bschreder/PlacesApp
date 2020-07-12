using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DomainEntities.Place
{
    [JsonObject(MemberSerialization.OptOut, NamingStrategyType = typeof(CamelCaseNamingStrategy), ItemNullValueHandling = NullValueHandling.Ignore)]
    public class PlacesResponse
    {

        public class Root
        {
            public string Status { get; set; }
            public Prediction[] Predictions { get; set; }
        }

        public class Prediction
        {
            public string Description { get; set; }
            public int Distance_meters { get; set; }
            public string Id { get; set; }
            public Matched_Substrings[] Matched_substrings { get; set; }
            public string Place_id { get; set; }
            public string Reference { get; set; }
            public Term[] Terms { get; set; }
            public string[] Types { get; set; }
            public Structured_Formatting Structured_formatting { get; set; }
        }

        public class Structured_Formatting
        {
            public string Main_text { get; set; }
            public Main_Text_Matched_Substrings[] Main_text_matched_substrings { get; set; }
            public string Secondary_text { get; set; }
        }

        public class Main_Text_Matched_Substrings
        {
            public int Length { get; set; }
            public int Offset { get; set; }
        }

        public class Matched_Substrings
        {
            public int Length { get; set; }
            public int Offset { get; set; }
        }

        public class Term
        {
            public int Offset { get; set; }
            public string Value { get; set; }
        }

    }
}