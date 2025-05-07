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

            // Perform manual validation for required fields
            if (string.IsNullOrEmpty(farmer.FirstName))
                ModelState.AddModelError("FirstName", "First Name is required");

            if (string.IsNullOrEmpty(farmer.LastName))
                ModelState.AddModelError("LastName", "Last Name is required");

            if (string.IsNullOrEmpty(farmer.Username))
                ModelState.AddModelError("Username", "Username is required");

            if (string.IsNullOrEmpty(farmer.Password))
                ModelState.AddModelError("Password", "Password is required");

            if (string.IsNullOrEmpty(farmer.Email))
                ModelState.AddModelError("Email", "Email is required");

            if (string.IsNullOrEmpty(farmer.FarmName))
                ModelState.AddModelError("FarmName", "Farm Name is required");

            if (string.IsNullOrEmpty(farmer.FarmLocation))
                ModelState.AddModelError("FarmLocation", "Farm Location is required");

            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.Username == farmer.Username))
            {
                ModelState.AddModelError("Username", "This username is already taken.");
            }

            // Check if model is valid after our custom validations
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(farmer);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Farmer added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the exception
                    ModelState.AddModelError("", "An error occurred while creating the farmer. Please try again.");
                }
            }

            // Store non-sensitive form data in TempData to be displayed in the form
            TempData["FormData"] = new
            {
                farmer.FirstName,
                farmer.LastName,
                farmer.Username,
                farmer.Email,
                farmer.FarmName,
                farmer.FarmLocation,
                farmer.FarmDescription
            };

            // If we get here, something failed, show error summary
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

        // GET: Farmers/Profile - For farmers to view their own profile
        public async Task<IActionResult> Profile()
        {
            // Check if user is a farmer
            if (HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Index", "Home");
            }

            int farmerId = int.Parse(HttpContext.Session.GetString("UserId"));

            var farmer = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == farmerId);

            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        // GET: Farmers/Edit
        public async Task<IActionResult> Edit()
        {
            // Check if user is a farmer
            if (HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Index", "Home");
            }

            int farmerId = int.Parse(HttpContext.Session.GetString("UserId"));

            var farmer = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == farmerId);

            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        // POST: Farmers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User farmer, string? NewPassword)
        {
            try
            {
                // Check if user is a farmer
                if (HttpContext.Session.GetString("Role") != "Farmer")
                {
                    TempData["ErrorMessage"] = "Only farmers can edit their profiles.";
                    return RedirectToAction("Index", "Home");
                }

                // Ensure we're editing the correct user (security check)
                int farmerId = int.Parse(HttpContext.Session.GetString("UserId"));
                if (farmer.UserId != farmerId)
                {
                    TempData["ErrorMessage"] = "You can only edit your own profile.";
                    return Forbid();
                }

                // Get the existing farmer record
                var existingFarmer = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId == farmerId);

                if (existingFarmer == null)
                {
                    TempData["ErrorMessage"] = "User not found.";
                    return NotFound();
                }

                // Validate required fields
                if (string.IsNullOrEmpty(farmer.FirstName))
                    ModelState.AddModelError("FirstName", "First Name is required");

                if (string.IsNullOrEmpty(farmer.LastName))
                    ModelState.AddModelError("LastName", "Last Name is required");

                if (string.IsNullOrEmpty(farmer.Email))
                    ModelState.AddModelError("Email", "Email is required");

                if (string.IsNullOrEmpty(farmer.FarmName))
                    ModelState.AddModelError("FarmName", "Farm Name is required");

                if (string.IsNullOrEmpty(farmer.FarmLocation))
                    ModelState.AddModelError("FarmLocation", "Farm Location is required");

                // Check if another user already has this username (if username was changed)
                if (farmer.Username != existingFarmer.Username &&
                    await _context.Users.AnyAsync(u => u.Username == farmer.Username))
                {
                    ModelState.AddModelError("Username", "This username is already taken.");
                }

                if (ModelState.IsValid)
                {
                    // Update the properties
                    existingFarmer.FirstName = farmer.FirstName;
                    existingFarmer.LastName = farmer.LastName;
                    existingFarmer.Username = farmer.Username;
                    existingFarmer.Email = farmer.Email;
                    existingFarmer.FarmName = farmer.FarmName;
                    existingFarmer.FarmLocation = farmer.FarmLocation;
                    existingFarmer.FarmDescription = farmer.FarmDescription;

                    // Only update password if a new one is provided
                    if (!string.IsNullOrEmpty(NewPassword))
                    {
                        existingFarmer.Password = NewPassword;
                    }

                    // Save changes to database
                    _context.Update(existingFarmer);
                    await _context.SaveChangesAsync();

                    // Update session data
                    HttpContext.Session.SetString("Username", existingFarmer.Username);
                    HttpContext.Session.SetString("FullName", $"{existingFarmer.FirstName} {existingFarmer.LastName}");

                    TempData["SuccessMessage"] = "Your profile has been updated successfully!";
                    return RedirectToAction(nameof(Profile));
                }
                else
                {
                    // If ModelState is invalid, redisplay the form with error messages
                    return View(farmer);
                }
            }
            catch (Exception ex)
            {
                // Add error message to TempData for display
                TempData["ErrorMessage"] = $"An error occurred while updating your profile: {ex.Message}";
                return View(farmer);
            }
        }
    }
}