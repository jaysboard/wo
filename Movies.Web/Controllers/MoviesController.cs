using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Movies.Web.Models.JSONLD;
using Movies.Web.Models.Movies;
using RestSharp;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        ILogger<MoviesController> _logger;
        string _strOMDbAPI = "";

        public MoviesController(
            ILogger<MoviesController> logger,
            IConfiguration configuration
        )
        {            
            _logger = logger;
            _logger.LogInformation("MoviesController");

            _strOMDbAPI = configuration.GetValue<string>("AppSettings:OMDbAPI");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MovieResult(string s, string y = "", string page = "")
        {
            string strURL = $"https://www.omdbapi.com/?apikey={_strOMDbAPI}&s={s}{(!string.IsNullOrWhiteSpace(y)? "&y=" + y : "")}{(!string.IsNullOrWhiteSpace(page) ? "&page=" + page : "")}";
            var client = new RestClient(strURL);

            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");

            var response = client.Execute<SearchResult>(request);
            var searchResult = response.Data;
            searchResult.SearchString = s;
            searchResult.SearchYear = y;

            if (searchResult?.Response == true)
            {
                var jsonLDSummary = new JSONLDSummary();
                searchResult?.Search?.ForEach(movieInfo =>
                {
                    jsonLDSummary.itemListElement.Add(
                        new JSONLDSummary.ItemListElement
                        {
                            position = $"{jsonLDSummary.itemListElement.Count + 1}",
                            url = Url.Action("Details", "Movies", new { i = movieInfo.imdbID }, this.Request.Scheme)
                        }
                    );
                });

                searchResult.summaryJSONLD = jsonLDSummary;
            }

            int nCurrentPage = searchResult.CurrentPage;

            try
            {
                nCurrentPage = string.IsNullOrWhiteSpace(page) ? 1 : int.Parse(page);
            }
            catch (System.Exception)
            {

                nCurrentPage = searchResult.CurrentPage;
            }

            setPagination(searchResult, nCurrentPage);

            return View(searchResult);
        }

        private void setPagination(SearchResult searchResult, int nCurrentPage)
        {
            if (searchResult.totalResults > 0)
            {
                searchResult.CurrentPage = nCurrentPage;
                searchResult.Count = searchResult.totalResults;
                searchResult.TotalPages = (int)System.Math.Ceiling(decimal.Divide(searchResult.Count, searchResult.PageSize));
                searchResult.ShowPrevious = searchResult.CurrentPage > 1;
                searchResult.ShowNext = searchResult.CurrentPage < searchResult.TotalPages;
                searchResult.ShowFirst = searchResult.CurrentPage != 1;
                searchResult.ShowLast = (searchResult.TotalPages != 0) && (searchResult.CurrentPage != searchResult.TotalPages);
            }

        }

        public IActionResult Details(string i)
        {
            string strURL = $"https://www.omdbapi.com/?apikey={_strOMDbAPI}&i={i}";
            var client = new RestClient(strURL);

            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");

            var response = client.Execute<DetailResult>(request);
            var detailResult = response.Data;

            if (detailResult?.Response == true)
            {
                var jsonLDMovie = new Movie
                {
                    image = detailResult.Poster,
                    name = detailResult.Title,
                    aggregateRating = new AggregateRating 
                    {
                        author = new Author { type = "Organization", name = "IMDB" },
                        ratingValue = detailResult.imdbRating,
                        bestRating = "10", 
                        ratingCount = detailResult.imdbVotes
                    },
                    dateCreated = detailResult.Released,
                    director = new Director { name = detailResult.Director },
                    url = Url.Action("Details", "Movies", new { i = detailResult.imdbID }, this.Request.Scheme),
                    description = detailResult.Plot
                };

                detailResult.movie = jsonLDMovie;
            }

            return View(detailResult);
        }
    }
}
