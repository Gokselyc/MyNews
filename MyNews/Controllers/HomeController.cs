using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNews.Core.Models;
using Microsoft.EntityFrameworkCore;
using MyNews.Core.Data;
using MyNews.Models;

namespace MyNews.Controllers
{
    public class HomeController : Controller
    {
        private AplicationDbContext context;

        public HomeController(AplicationDbContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            //var news=(from n in context.News orderby n.PublishDate descending select n); sorgu linqu
            var news = context.News.Include(i=>i.Category).Where(n => n.IsPublished == true).OrderByDescending(o => o.PublishDate).Take(10).ToList(); //n=>n ler Lambda,Take(10) sadece 10 tane kaydı getirir.

            var categories = context.Categories.OrderBy(c => c.Name).Select(s => new CategoryViewModel { Id = s.Id, Name = s.Name, Count = s.News.Count }).ToList();

            ViewBag.Categories = categories;


            return View(news);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

    }
}
