/*  HomeController.cs
    In Class 1

    Revision History
    David Florez ID: 8820815, 2023.11.09: Created
*/
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http; // Needed to work with Sessions

using UserActionTrackingApp.Models;

namespace UserActionTrackingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Instantiates session and calls method to increase session counter on GET action
            var session = new TrackingSession(HttpContext.Session);
            session.IncreaseSessionCounter("Home", "Index");

            // Instantiates session and calls method to increase cookies counter on GET action
            var cookies = new TrackingCookies(Response.Cookies); // Using Response to set cookies
            cookies.IncreaseCookiesCounter("Home", "Index");

            return View();
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



// TODO: Examples of Sessions & Cookies
/*

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // TODO: Session Variable Creation. Can be accessed anywhere in the app
        HttpContext.Session.SetString("MyKey", "Conestoga Session");

        // TODO: TempData variable (similar to ViewBag, but data can only be viewd onced)
        TempData["SessionID"] = HttpContext.Session.Id;

        return View();
    }

    public IActionResult Privacy()
    {
        // TODO: Working with session variable in another action
        if (HttpContext.Session.GetString("MyKey") != null) // Checks session is not null
        {
            ViewBag.Data = HttpContext.Session.GetString("MyKey").ToString();
        }
        return View();
    }

    public IActionResult About()
    {
        // TODO: Working with session variable in another action
        if (HttpContext.Session.GetString("MyKey") != null) // Checks session is not null
        {
            ViewBag.Data = HttpContext.Session.GetString("MyKey").ToString();
        }
        return View();
    }

    public IActionResult Logout()
    {
        // TODO: Remove / Delete a SESSION
        if(HttpContext.Session.GetString("MyKey") != null)
        {
            HttpContext.Session.Remove("MyKey");
        }
        return View("Index"); // Renders Home/Index
    }

    // TODO: Working with Cookies
    // Used to show how to create a Cookie
    public IActionResult Create()
    {
        // TODO: Create Cookies + Cookie Options
        string key = "MyCookie";
        string value = "The cookie is here!!";
        CookieOptions cookieOptions = new CookieOptions();
        cookieOptions.Expires = DateTime.Now.AddDays(7); // Cookies expiration
        Response.Cookies.Append(key, value, cookieOptions); // Response back to the client sends the cookie

        return View("Index");
    }

    // Used to show how to read a Cookie
    public IActionResult Read()
    {
        // TODO: 
        string key = "MyCookie";
        var value = Request.Cookies[key];
        Console.WriteLine(value);

        return View("Index");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
*/