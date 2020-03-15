using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Models.JSONLD
{
    public class AggregateRating
    {
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; } = "AggregateRating";
        public string ratingValue { get; set; }
        public string bestRating { get; set; }
        public string ratingCount { get; set; }
        public Author author { get; set; }
    }
}
