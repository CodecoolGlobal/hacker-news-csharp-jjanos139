using System;
using HackerNewsClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Codecool.HackerNewsClient.Models;

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
        public ActionResult Jobs()
        {
            var publicationDate = new DateOnly(1953, 5, 10);
            var jobs = new Jobs() {Title = "Birth of Holy Mother", TimeAgo = publicationDate};
            return View(jobs);
        }
        public ActionResult Newest()
        {
            var publicationDate = new DateOnly(1987, 2, 9);
            var newest = new Newest() { Title = "Birth of God", Author = "Jankovics János", TimeAgo = publicationDate };
            return View(newest);
        }
        public ActionResult TopNews()
        {
            var publicationDate = new DateOnly(2001, 1, 1);
            var topNews = new TopNews() { Title = "Shrek", Author = "Eddie Murphy", TimeAgo = publicationDate };
            return View(topNews);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
