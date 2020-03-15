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
            return View();
        }

        public IActionResult MovieResult(string s)
        {
            string strURL = $"https://www.omdbapi.com/?apikey={_strOMDbAPI}&s={s}";
            var client = new RestClient(strURL);

            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");

            var response = client.Execute<SearchResult>(request);
            var searchResult = response.Data;

            if (searchResult?.Response == true)
            {
                var summaryJSONLD = new Summary();
                searchResult?.Search?.ForEach(movieInfo =>
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

                searchResult.summaryJSONLD = summaryJSONLD;
            }

            return View(searchResult);
        }

        public IActionResult Details(string i)
        {
            string strURL = $"https://www.omdbapi.com/?apikey={_strOMDbAPI}&i={i}";
            //var client = new RestClient(strURL);
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("content-type", "application/json");
            //var response = client.Execute<Details>(request);
            //var queryResult = response.Data;

            //return View(queryResult);

            var client = new RestClient(strURL);

            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");

            var response = client.Execute<DetailResult>(request);
            var detailResult = response.Data;

            if (detailResult?.Response == true)
            {
                var detailsJSONLD = new AllInOne();
                //detailResult?.Search?.ForEach(movieInfo =>
                //{
                //    detailsJSONLD.itemListElement.Add(
                //        new Summary.ItemListElement
                //        {
                //            @type = "ListItem",
                //            position = $"{detailsJSONLD.itemListElement.Count + 1}",
                //            url = Url.Action("Details", "Movies", new { i = movieInfo.imdbID }, this.Request.Scheme)
                //        }
                //    );
                //});
                //detailsJSONLD.

                detailsJSONLD.itemListElement.Add(
                    new AllInOne.ItemListElement
                    {
                        @type = "ListItem",
                        position = $"{detailsJSONLD.itemListElement.Count + 1}",
                        item = new AllInOne.Item
                        {
                            name = detailResult.Title,
                            image = detailResult.Poster,
                            dateCreated = detailResult.Released,
                            director = new AllInOne.Director { name = detailResult.Director },
                            url = Url.Action("Details", "Movies", new { i = detailResult.imdbID }, this.Request.Scheme)
                        }
                    }
                );

                detailResult.detailJSONLD = detailsJSONLD;
            }

            return View(detailResult);
        }
    }
}
