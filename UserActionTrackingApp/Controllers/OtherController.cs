/*  OtherController.cs
    In Class 1

    Revision History
    David Florez ID: 8820815, 2023.11.09: Created
*/
using Microsoft.AspNetCore.Mvc;

using UserActionTrackingApp.Models;

namespace UserActionTrackingApp.Controllers
{
    public class OtherController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // Instantiates session and calls method to increase session counter on GET action
            var session = new TrackingSession(HttpContext.Session);
            session.IncreaseSessionCounter("Other", "Index");

            // Instantiates session and calls method to increase cookies counter on GET action
            var cookies = new TrackingCookies(Response.Cookies); // Using Response to set cookies
            cookies.IncreaseCookiesCounter("Other", "Index");

            return View();
        }
    }
}
