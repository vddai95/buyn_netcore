using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace byin_netcore_business.Entity
{
    public class Repository
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
