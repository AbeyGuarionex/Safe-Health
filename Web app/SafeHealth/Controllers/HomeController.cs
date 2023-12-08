using Microsoft.AspNetCore.Mvc;
using SafeHealth.Models;
using System.Diagnostics;

namespace SafeHealth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        /// <summary>Action to display the Log out page if a successful log in is done
        /// if not it returns to the log in page (index).</summary>
        [Route("LogOut")]
        public IActionResult LogOut()
        {
            string checkIfLogedIn = HttpContext.Session.GetString("userEmail");

            ViewBag.userType = HttpContext.Session.GetString("userType");

            if (checkIfLogedIn != null)
            {
                Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
                Response.Headers.Add("Pragma", "no-cache");
                Response.Headers.Add("Expires", "0");

                // Add a unique query string to the URL to prevent caching
                return View("LogOut", new { version = Guid.NewGuid().ToString() });
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }

        }



    }
}

  
