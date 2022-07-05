using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Codecool.HackerNewsClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Codecool.HackerNewsClient.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            string apiUrl = "https://api.hnpwa.com/v0/news/1.json";
            var topNewsResponse = await GetData(apiUrl);
            List<Models.Index> topNewsList = JsonConvert.DeserializeObject<List<Models.Index>>(topNewsResponse);
            return View(topNewsList);
        }

        public async Task<string> TopNews(int? page)
        {
            string apiUrl = $"https://api.hnpwa.com/v0/news/{page}.json";
            return await GetData(apiUrl);
        }

        public async Task<string> Newest(int? page)
        {
            string apiUrl = $"https://api.hnpwa.com/v0/newest/{page}.json";
            return await GetData(apiUrl);
        }

        public async Task<string> Jobs(int? page)
        {
            string apiUrl = $"https://api.hnpwa.com/v0/jobs/{page}.json";
            return await GetData(apiUrl);
        }

        private static async Task<string> GetData(string apiUrl)
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}