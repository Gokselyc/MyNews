using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNews.Core.Models;
using Microsoft.AspNetCore.Http;
using MyNews.Admin.Models;

namespace MyNews.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Security.LoginCheck(HttpContext);


            return View();
        }

        public IActionResult About()
        {
            Security.LoginCheck(HttpContext);
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            Security.LoginCheck(HttpContext);
            ViewData["Message"] = "Your contact page.";

            return View();
        }

   
    }
}
