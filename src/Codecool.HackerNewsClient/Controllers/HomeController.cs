using System;
using System.Collections.Generic;
using HackerNewsClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Codecool.HackerNewsClient.Models;
using Newtonsoft.Json;

namespace HackerNewsClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Jobs(int? id)
        {
            if (id == null)
                id = 1;
            const string baseurl = "https://api.hnpwa.com/";

            List<Jobs> jobsList = new List<Jobs>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"v0/jobs/{id}.json");

                if (Res.IsSuccessStatusCode)
                {
                    var jobsResponse = Res.Content.ReadAsStringAsync().Result;

                    jobsList = JsonConvert.DeserializeObject<List<Jobs>>(jobsResponse);
                }

                return View(jobsList);
            }
        }

        public ActionResult Index()
        {
            return RedirectToAction("TopNews");
        }
        public async Task<ActionResult> Newest(int? id)
        {
            if (id == null)
                id = 1;
            const string baseurl = "https://api.hnpwa.com/";

            List<Newest> newestList = new List<Newest>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"v0/newest/{id}.json");

                if (Res.IsSuccessStatusCode)
                {
                    var newestResponse = Res.Content.ReadAsStringAsync().Result;

                    newestList = JsonConvert.DeserializeObject<List<Newest>>(newestResponse);
                }

                return View(newestList);
            }
        }

        public async Task<ActionResult> TopNews(int? id)
        {
            if (id == null)
                id = 1;
            const string baseurl = "https://api.hnpwa.com/";

            List<Codecool.HackerNewsClient.Models.TopNews> topNewsList = new List<Codecool.HackerNewsClient.Models.TopNews>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync($"v0/news/{id}.json");

                if (Res.IsSuccessStatusCode)
                {
                    var topNewsResponse = Res.Content.ReadAsStringAsync().Result;

                    topNewsList = JsonConvert.DeserializeObject<List<Codecool.HackerNewsClient.Models.TopNews>>(topNewsResponse);
                }

                return View(topNewsList);
            }
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
