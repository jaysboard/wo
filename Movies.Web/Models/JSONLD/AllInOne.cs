using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Movies.Web.Models.JSONLD
{
    public class AllInOne
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
            public Item item { get; set; }
        }

        public class Item
        {
            [JsonProperty(PropertyName = "@type")]
            public string @type { get; set; } = "Movie";
            public string url { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public string dateCreated { get; set; }
            public Director director { get; set; }
            public Review review { get; set; }
            public AggregateRating aggregateRating { get; set; }
        }

        public class Director
        {
            [JsonProperty(PropertyName = "@type")]
            public string @type { get; set; } = "Person";
            public string name { get; set; }
        }

        public class Review
        {
            [JsonProperty(PropertyName = "@type")]
            public string @type { get; set; } = "Review";
            public ReviewRating reviewRating { get; set; }
            public Author author { get; set; }
            public string reviewBody { get; set; }
        }

        public class AggregateRating
        {
            [JsonProperty(PropertyName = "@type")]
            public string @type { get; set; } = "AggregateRating";
            public string ratingValue { get; set; }
            public string bestRating { get; set; }
            public string ratingCount { get; set; }
        }

        public class ReviewRating
        {
            [JsonProperty(PropertyName = "@type")]
            public string @type { get; set; } = "Rating";
            public string ratingValue { get; set; }
        }

        public class Author
        {
            [JsonProperty(PropertyName = "@type")]
            public string @type { get; set; } = "Person";
            public string name { get; set; }
        }
    }
}
