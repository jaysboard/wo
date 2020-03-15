using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Models.JSONLD
{
    public class ReviewRating
    {
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; } = "Rating";
        public string ratingValue { get; set; }
    }
}
