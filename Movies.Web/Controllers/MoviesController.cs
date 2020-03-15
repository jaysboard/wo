using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Movies.Web.Models.JSONLD;
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
            var response = client.Execute<SearchResult>(request);
            var queryResult = response.Data;

            //string json = JsonConvert.SerializeObject(queryResult.Content);
            //return View(new SearchResult { Title=movieToSearch, Year=2019, Poster="testImageUrl" });

            if (queryResult?.Response == true)
            {
                var summaryJSONLD = new Summary();
                queryResult?.Search?.ForEach(movieInfo =>
                {
                    summaryJSONLD.itemListElement.Add(
                        new Summary.ItemListElement
                        {
                            @type = "ListItem",
                            position = $"{summaryJSONLD.itemListElement.Count + 1}",
                            url = Url.Action("Details", "Movies", new { i = movieInfo.imdbID }, this.Request.Scheme)
                        }
                    );
                });

                //HtmlGenericControl newControl = new HtmlGenericControl("someTag");
                //newControl.Attributes["someAttr"] = "some value";
                //Page.Header.Controls.Add(newControl);

                //Response.Headers.Add("X-Total-Count", "20");
                //TagHelperContext

                queryResult.summaryJSONLD = summaryJSONLD;
            }

            return View(queryResult);
        }

        public IActionResult Details(string i)
        {
            string strUrlMovieDetail = $"https://www.omdbapi.com/?apikey={_strOMDbAPI}&i={i}";
            var client = new RestClient(strUrlMovieDetail);
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            var response = client.Execute<Details>(request);
            var queryResult = response.Data;

            return View(queryResult);
        }
    }
}
