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
        public Summary summaryJSONLD { get; set; }
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
