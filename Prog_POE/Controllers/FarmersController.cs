using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prog_POE.Data;
using Prog_POE.Models;

namespace Prog_POE.Controllers
{
    public class FarmersController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public FarmersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Farmers - List all farmers (Employee only)
        public async Task<IActionResult> Index()
        {
            // Check if the user is an employee
            if (HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Index", "Home");
            }

            var farmers = await _context.Users
                .Where(u => u.Role == "Farmer")
                .ToListAsync();

            return View(farmers);
        }

        // GET: Farmers/Create - Form to create a new farmer (Employee only)
        public IActionResult Create()
        {
            // Check if the user is an employee
            if (HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Farmers/Create - Create a new farmer (Employee only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User farmer)
        {
            // Check if the user is an employee
            if (HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Index", "Home");
            }

            // Set role to Farmer
            farmer.Role = "Farmer";

            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.Username == farmer.Username))
            {
                ModelState.AddModelError("Username", "This username is already taken.");
                return View(farmer);
            }

            if (ModelState.IsValid)
            {
                _context.Add(farmer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Farmer added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(farmer);
        }

        // GET: Farmers/Details/5 - View farmer details and products
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Users
                .Include(f => f.Products)
                .FirstOrDefaultAsync(m => m.UserId == id && m.Role == "Farmer");

            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }
    }
}