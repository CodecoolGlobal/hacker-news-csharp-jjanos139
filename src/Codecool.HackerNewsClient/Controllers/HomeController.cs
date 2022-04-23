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
using Microsoft.AspNetCore.Routing;
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

        public async Task<ActionResult> Index()
        {
            const string baseurl = "https://api.hnpwa.com/";

            List<Codecool.HackerNewsClient.Models.Index> topNewsList = new();

            using var client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync($"v0/news/1.json");

            if (Res.IsSuccessStatusCode)
            {
                var topNewsResponse = Res.Content.ReadAsStringAsync().Result;

                topNewsList = JsonConvert.DeserializeObject<List<Codecool.HackerNewsClient.Models.Index>>(topNewsResponse);
            }

            return View(topNewsList);
        }

        //[HttpGet]
            //public ActionResult Index()
            //{
            //    return RedirectToAction("TopNews");
            //}

            //[HttpGet]
            //public async Task<ActionResult> TopNews(int? page)
            //{
            //    if (page == null)
            //        page = 1;
            //    const string baseurl = "https://api.hnpwa.com/";

            //    List<Codecool.HackerNewsClient.Models.TopNews> topNewsList = new List<Codecool.HackerNewsClient.Models.TopNews>();

            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri(baseurl);

            //        client.DefaultRequestHeaders.Clear();
            //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //        HttpResponseMessage Res = await client.GetAsync($"v0/news/{page}.json");

            //        if (Res.IsSuccessStatusCode)
            //        {
            //            var topNewsResponse = Res.Content.ReadAsStringAsync().Result;

            //            topNewsList = JsonConvert.DeserializeObject<List<Codecool.HackerNewsClient.Models.TopNews>>(topNewsResponse);
            //        }

            //        ViewData["page"] = page;

            //        return View(topNewsList);
            //    }
            //}

            public async Task<string> TopNews(int? page)
            {
                string apiURL = $"https://api.hnpwa.com/v0/news/{page}.json";

                using HttpClient client = new();
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiURL);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                return null;
            }
            public async Task<string> Newest(int? page)
            {
                string apiURL = $"https://api.hnpwa.com/v0/newest/{page}.json";

                using HttpClient client = new();
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiURL);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                return null;
            }
            public async Task<string> Jobs(int? page)
            {
                string apiURL = $"https://api.hnpwa.com/v0/jobs/{page}.json";

                using HttpClient client = new();
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiURL);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                return null;
            }

            //[HttpGet]
            //public async Task<ActionResult> Jobs(int? page)
            //{
            //    if (page == null)
            //        page = 1;
            //    const string baseurl = "https://api.hnpwa.com/";

            //    List<Jobs> jobsList = new List<Jobs>();

            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri(baseurl);

            //        client.DefaultRequestHeaders.Clear();
            //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //        HttpResponseMessage Res = await client.GetAsync($"v0/jobs/{page}.json");

            //        if (Res.IsSuccessStatusCode)
            //        {
            //            var jobsResponse = Res.Content.ReadAsStringAsync().Result;

            //            jobsList = JsonConvert.DeserializeObject<List<Jobs>>(jobsResponse);
            //        }

            //        ViewData["page"] = page;

            //        return View(jobsList);
            //    }
            //}

            //[HttpGet]
            //public async Task<ActionResult> Newest(int? page)
            //{
            //    if (page == null)
            //        page = 1;
            //    const string baseurl = "https://api.hnpwa.com/";

            //    List<Newest> newestList = new List<Newest>();

            //    using (var client = new HttpClient())
            //    {
            //        client.BaseAddress = new Uri(baseurl);

            //        client.DefaultRequestHeaders.Clear();
            //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //        HttpResponseMessage Res = await client.GetAsync($"v0/newest/{page}.json");

            //        if (Res.IsSuccessStatusCode)
            //        {
            //            var newestResponse = Res.Content.ReadAsStringAsync().Result;

            //            newestList = JsonConvert.DeserializeObject<List<Newest>>(newestResponse);
            //        }

            //        ViewData["page"] = page;

            //        return View(newestList);
            //    }
            //}

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
