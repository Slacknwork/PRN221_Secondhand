using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PRN221_Secondhand.Models;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;

namespace PRN221_Secondhand.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [ActionName("Index")]
        public IActionResult Index()
        {
            PostRepository _postRepo = new PostRepository();
            var _listPost = _postRepo.GetAll().Where(item => item.Status != 0).ToList();
            return View(_listPost);
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
        [HttpGet]
        [ActionName("Search")]
        public IActionResult Search(string key)
        {
            PostRepository _postRepo = new PostRepository();
            TempData["key"] = key;
            var _listPost = _postRepo.GetAll().Where(item => item.Status != 0 && item.Name.Contains(key)).ToList();
            return View(_listPost);
        }
        [HttpGet]
        [ActionName("Filter")]
        public IActionResult Filter(string cate)
        {
            if (cate.Equals("all"))
            {
                return RedirectToAction("Index", "Home");
            }
            PostRepository _postRepo = new PostRepository();
            var _listPost = _postRepo.GetAll().Where(item => item.Status != 0 && item.Category.Name.Equals(cate)).ToList();
            return View(_listPost);
        }
    }
}
