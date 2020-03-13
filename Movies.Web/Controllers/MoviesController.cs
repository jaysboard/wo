using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Movies.Web.Models.Movies;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serialization.Xml;
using System.Collections.Generic;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        ILogger<MoviesController> _logger;
        IConfiguration _configuration;

        string _strOMDbAPI = "";

        public MoviesController(
            ILogger<MoviesController> logger,
            IConfiguration configuration
        )
        {            
            _logger = logger;

            _configuration = configuration;
            _strOMDbAPI = _configuration.GetValue<string>("AppSettings:OMDbAPI");

            _logger.LogInformation("MoviesController");            
        }

        public IActionResult Index()
        {
            //var model = new SearchResults();
            //model.LoadSampleData();
            //return View(model);
            return View();
        }

        public IActionResult MovieResult(string s)
        {
            string strUrlMovieSearh = $"https://www.omdbapi.com/?apikey={_strOMDbAPI}&s={s}";
            var client = new RestClient(strUrlMovieSearh);
            //var request = new RestRequest(Method.GET);
            //IRestResponse response = client.Execute(request);
            //response.get
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            //client.UseDotNetXmlSerializer();
            //var queryResult = client.Execute<SearchResult>(request).Content;
            //var queryResult2 = client.Execute<SearchResult>(request);
            var queryResult = client.Execute<SearchResult>(request).Data;

            //string json = JsonConvert.SerializeObject(queryResult.Content);

            //return View(new SearchResult { Title=movieToSearch, Year=2019, Poster="testImageUrl" });
            return View(queryResult);
        }

        public IActionResult Details(string i)
        {
            string strUrlMovieDetail = $"https://www.omdbapi.com/?apikey={_strOMDbAPI}&i={i}";
            var client = new RestClient(strUrlMovieDetail);
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            var queryResult = client.Execute<Details>(request).Data;

            return View(queryResult);
        }
    }
}
