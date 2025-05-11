using Microsoft.AspNetCore.Mvc;
using Prog_POE.Data;
using Prog_POE.Models;
using System.Diagnostics;
namespace Prog_POE.Controllers
{
    // Controller responsible for handling the main pages of the application
    // Inherits from BaseController to ensure authentication for protected pages
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        // Constructor with dependency injection for logging and database access
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Main dashboard page displaying summary statistics
        public IActionResult Index()
        {
            // Calculate summary statistics for the dashboard
            // Count of farmers registered in the system
            ViewBag.FarmerCount = _context.Users.Count(u => u.Role == "Farmer");

            // Total number of products available on the platform
            ViewBag.ProductCount = _context.Products.Count();

            // Count of products that are certified organic
            // Used to highlight sustainability focus of the platform
            ViewBag.OrganicProductCount = _context.Products.Count(p => p.IsOrganic);

            return View();
        }

        // Privacy policy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Error handling page with request ID for troubleshooting
        // ResponseCache attributes prevent caching of error pages
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}