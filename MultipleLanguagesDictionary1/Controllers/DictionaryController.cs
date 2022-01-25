using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultipleLanguagesDictionary1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MultipleLanguagesDictionary1.Controllers
{
    [Authorize(Roles = RoleName.Member)]
    public class DictionaryController : Controller
    {
        private readonly ILogger<DictionaryController> _logger;

        public DictionaryController(ILogger<DictionaryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index(string? word, string? lang = "en")
        {
            try
            {
                if (!string.IsNullOrEmpty(word))
                {
                    string path = "https://api.dictionaryapi.dev/api/v2/entries/en/hello";
                    string replace = path.Replace("en/hello", lang + "/" + word);
                    object dictionary = getDictionary(replace);

                    JArray jArray = JArray.Parse(dictionary.ToString());

                    IList<Dictionary> dictionaries = JsonConvert.DeserializeObject<IList<Dictionary>>(dictionary.ToString());

                    var meanings = dictionaries[0].meanings;

                    var countDic = dictionaries.Count();

                    var countPart = dictionaries[0].meanings.Select(s => s.partOfSpeech).Count();

                    var syn = dictionaries[0].meanings[0].definitions[0].synonyms;

                    ViewBag.countPart = countPart;

                    ViewBag.data = dictionaries;

                    ViewBag.mean = meanings;

                    ViewBag.syn = syn;
                    
                    ViewBag.countSyn = syn!.ToList().Count();

                    ViewBag.count = countDic;

                    ViewBag.lang = lang;
                    return View();
                }
                else
                {
                    ViewData["word"] = "null";
                    return View();
                }
            }
            catch (JsonReaderException e)
            {
                ViewData["data"] = "null";
                ViewData["mean"] = "null";
                return View();
            }
        }

        public IActionResult Translate()
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

        public string getWord(string word, string? lang = "en")
        {
            string path = "https://api.dictionaryapi.dev/api/v2/entries/en/word";
            string replace = path.Replace("en/word", lang + "/" + word);
            return replace;
        }

    }
}