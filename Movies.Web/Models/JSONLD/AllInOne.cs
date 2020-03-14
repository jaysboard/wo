using System;
using System.Collections.Generic;

namespace Movies.Web.Models.JSONLD
{
    public class AllInOne
    {
        public string @context { get; set; } = "https://schema.org";
        public string @type { get; set; } = "ItemList";
        public List<ItemListElement> itemListElement { get; set; } = new List<ItemListElement>();

        public class ItemListElement
        {
            public string @type { get; set; }
            public string position { get; set; }
            public Item item { get; set; }
        }

        public class Item
        {
            public string @type { get; set; }
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
            public string @type { get; set; }
            public string name { get; set; }
        }

        public class Review
        {
            public string @type { get; set; }
            public ReviewRating reviewRating { get; set; }
            public Author author { get; set; }
            public string reviewBody { get; set; }
        }

        public class AggregateRating
        {
            public string @type { get; set; }
            public string ratingValue { get; set; }
            public string bestRating { get; set; }
            public string ratingCount { get; set; }
        }

        public class ReviewRating
        {
            public string @type { get; set; }
            public string ratingValue { get; set; }
        }

        public class Author
        {
            public string @type { get; set; }
            public string name { get; set; }
        }
    }
}
