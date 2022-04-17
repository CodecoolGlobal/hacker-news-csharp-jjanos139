using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Codecool.HackerNewsClient.Models;
using Microsoft.AspNetCore.Mvc;

namespace Codecool.HackerNewsClient.Controllers
{
  public class TopNewsController : ControllerBase
  {
      public class HomeController : Controller
      {
          //Hosted web API REST Service base url  
          string Baseurl = "https://api.hnpwa.com/v0/news/1";
          public async Task<ActionResult> Index()
          {
              List<TopNews> TopNewsInfo = new List<TopNews>();

              using (var client = new HttpClient())
              {
                  //Passing service base url  
                  client.BaseAddress = new Uri(Baseurl);

                  client.DefaultRequestHeaders.Clear();
                  //Define request data format  
                  client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                  //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                  HttpResponseMessage Res = await client.GetAsync("api/TopNews");

                  //Checking the response is successful or not which is sent using HttpClient  
                  if (Res.IsSuccessStatusCode)
                  {
                      //Storing the response details recieved from web api   
                      var TopNewsResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        TopNewsInfo = JsonConvert.DeserializeObject<List<TopNews>>(TopNewsResponse);

                  }
                  //returning the employee list to view  
                  return View(TopNewsInfo);
              }
          }
      }
    }
}
