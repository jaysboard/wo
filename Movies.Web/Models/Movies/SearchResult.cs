using Movies.Web.Models.JSONLD;
using System.Collections.Generic;

namespace Movies.Web.Models.Movies
{
    public class SearchResult
    {
        public List<MovieInfo> Search { get; set; }
        public int totalResults { get; set; }
        public bool Response { get; set; }
        public string Error { get; set; }

        public JSONLDSummary summaryJSONLD { get; set; }
        public string SearchString { get; set; }
        public string SearchYear { get; set; }

        // pagination
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public int TotalPages { get; set; } = 0;
        public bool ShowPrevious { get; set; } = false;
        public bool ShowNext { get; set; } = false;
        public bool ShowFirst { get; set; } = false;
        public bool ShowLast { get; set; } = false;
    }

    public class MovieInfo
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
