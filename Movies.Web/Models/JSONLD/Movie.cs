using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Models.JSONLD
{
    public class Movie
    {
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; } = "Movie";
        public string url { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string dateCreated { get; set; }
        public Director director { get; set; }
        public Review review { get; set; }
        public AggregateRating aggregateRating { get; set; }

        public string description { get; set; }
    }
}
