using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Models.JSONLD
{

    public class Review
    {
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; } = "Review";
        public ReviewRating reviewRating { get; set; }
        public Author author { get; set; }
        public string reviewBody { get; set; }
    }
}
