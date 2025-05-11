using Microsoft.AspNetCore.Mvc;
using Prog_POE.Data;
using Prog_POE.Models;
using System.Diagnostics;
namespace Prog_POE.Controllers
{


    /*
--------------------------------Student Information----------------------------------
STUDENT NO.: ST10343093
Name: Arshad Shoaib Bhula
Course: BCAD Year 3
Module: Programming 3A - ENTERPRISE APPLICATION DEVELOPMENT 
Module Code: PROG7311
Assessment: Portfolio of Evidence (POE) Part 2
Github repo link: https://github.com/ST10343093/Prog_POE
--------------------------------Student Information----------------------------------

==============================Code Attribution==================================

MVC APP
Author: Microsoft
Link: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-9.0&tabs=visual-studio
Date Accessed: 03 May 2025

==============================Code Attribution==================================

*/


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