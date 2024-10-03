using ASP5.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;

namespace ASP5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetCookie(string value, DateTime expirationDate)
        {
            var options = new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = expirationDate
            };

            Response.Cookies.Append("UserValue", value, options);

            return Ok("Cookie was add");
        }

        public IActionResult CheckCookie()
        {
            string cookieValue = Request.Cookies["UserValue"];
            if (!string.IsNullOrEmpty(cookieValue))
            {
                return Ok("Cookie value: " + cookieValue);
            }
            else
            {
                throw new Exception("Coockie not found");
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
