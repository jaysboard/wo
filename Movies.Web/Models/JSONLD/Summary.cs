using System;
using System.Collections.Generic;

namespace Movies.Web.Models.JSONLD
{
    public class Summary
    {
        public string @context { get; set; } = "https://schema.org";
        public string @type { get; set; } = "ItemList";
        public List<ItemListElement> itemListElement { get; set; } = new List<ItemListElement>();

        public class ItemListElement
        {
            public string @type { get; set; }
            public string position { get; set; }
            public string url { get; set; }
        }
    }
}
