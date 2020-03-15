using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Movies.Web.Models.JSONLD
{
    public class JSONLDSummary
    {
        [JsonProperty(PropertyName = "@context")]
        public string context { get; set; } = "https://schema.org";
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; } = "ItemList";
        public List<ItemListElement> itemListElement { get; set; } = new List<ItemListElement>();

        public class ItemListElement
        {
            [JsonProperty(PropertyName = "@type")]
            public string type { get; set; } = "ListItem";
            public string position { get; set; }
            public string url { get; set; }
        }
    }
}
