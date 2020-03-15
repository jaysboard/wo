using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Models.JSONLD
{
    public class Author
    {
        [JsonProperty(PropertyName = "@type")]
        public string type { get; set; } = "Person";
        public string name { get; set; }
    }
}
