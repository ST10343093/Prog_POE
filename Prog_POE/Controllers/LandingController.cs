using Microsoft.AspNetCore.Mvc;

namespace Prog_POE.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}