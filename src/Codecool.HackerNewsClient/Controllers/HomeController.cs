using System;
using System.Collections.Generic;
using HackerNewsClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Codecool.HackerNewsClient.Controllers;
using Codecool.HackerNewsClient.Models;
using Newtonsoft.Json;
using String = System.String;

namespace HackerNewsClient.Controllers
{
    /// <summary>
    /// HomeController is a generic controller responsible for communicating with
    /// external or internal data sources (API or other data services).
    /// It contains methods communicating with the external API and
    /// serializing the data into News object.
    /// The methods return ActionResult which generates respective HTML page (View)
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// returns index page with top news
        /// </summary>
        /// <param name="page"> parameter of current page index </param>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{ 
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;

        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}
        public ActionResult Jobs()
        {
            var publicationDate = new DateOnly(1953, 5, 10);
            var jobs = new Jobs() { Title = "Birth of Holy Mother", TimeAgo = publicationDate };
            return View(jobs);
        }
        public ActionResult Newest()
        {
            var publicationDate = new DateOnly(1987, 2, 9);
            var newest = new Newest() { Title = "Birth of God", Author = "Jankovics János", TimeAgo = publicationDate };
            return View(newest);
        }
        public async Task<ActionResult> TopNews()
        {
            const string baseurl = "https://api.hnpwa.com/";

            List<TopNews> topNewsInfo = new List<TopNews>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("v0/news/1.json");

                if (Res.IsSuccessStatusCode)
                {
                    var topNewsResponse = Res.Content.ReadAsStringAsync().Result;

                    topNewsInfo = JsonConvert.DeserializeObject<List<TopNews>>(topNewsResponse);

                }

                return View(topNewsInfo);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
