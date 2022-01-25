using Microsoft.AspNetCore.Mvc;
using MultipleLanguagesDictionary1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net;

namespace MultipleLanguagesDictionary1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string word)
        {
            return View();
        }

        public object getDictionary(string path)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    return JsonConvert.DeserializeObject<object>(
                        webClient.DownloadString(path)
                    );
                }
            }
            catch (WebException e)
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}