using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prog_POE.Data;
using Prog_POE.Models;
using Prog_POE.Helpers;

namespace Prog_POE.Controllers
{
    // Controller for managing farmer profiles and information
    // Inherits from BaseController for authentication requirements
    public class FarmersController : BaseController
    {
        private readonly ApplicationDbContext _context;

        // Constructor with database context injection for database operations
        public FarmersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Farmers - List all farmers (Employee only)
        // Displays a list of all registered farmers in the system
        public async Task<IActionResult> Index()
        {
            // Role-based authorization check - only employees can access this page
            if (HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Index", "Home");
            }

            // Retrieve all users with Farmer role from the database
            var farmers = await _context.Users
                .Where(u => u.Role == "Farmer")
                .ToListAsync();

            return View(farmers);
        }

        // GET: Farmers/Create - Form to create a new farmer (Employee only)
        // Displays the form for employees to add new farmer accounts
        public IActionResult Create()
        {
            // Role-based authorization check - only employees can create farmers
            if (HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Farmers/Create - Create a new farmer (Employee only)
        // Processes the form submission to add a new farmer to the system
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User farmer)
        {
            // Role-based authorization check - only employees can create farmers
            if (HttpContext.Session.GetString("Role") != "Employee")
            {
                return RedirectToAction("Index", "Home");
            }

            // Set role to Farmer for the new user
            farmer.Role = "Farmer";

            // Perform manual validation for required fields to ensure data integrity
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

            // Check for username uniqueness to prevent duplicate accounts
            if (await _context.Users.AnyAsync(u => u.Username == farmer.Username))
            {
                ModelState.AddModelError("Username", "This username is already taken.");
            }

            // Check if model is valid after our custom validations
            if (ModelState.IsValid)
            {
                try
                {
                    // Security: Hash the password before storing in database
                    farmer.Password = PasswordHelper.HashPassword(farmer.Password);

                    // Add and save the new farmer to the database
                    _context.Add(farmer);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Farmer added successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Error handling for database operations
                    ModelState.AddModelError("", "An error occurred while creating the farmer. Please try again.");
                }
            }

            // Store non-sensitive form data in TempData to preserve values if validation fails
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

            // If we get here, validation failed or an error occurred
            return View(farmer);
        }

        // GET: Farmers/Details/5 - View farmer details and products
        // Displays detailed information about a specific farmer and their products
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve farmer with their products using eager loading
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
        // Allows farmers to see their current profile information
        public async Task<IActionResult> Profile()
        {
            // Role-based authorization check - only farmers can see their profile
            if (HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Index", "Home");
            }

            // Get the current farmer's ID from session
            int farmerId = int.Parse(HttpContext.Session.GetString("UserId"));

            // Retrieve the farmer's profile information
            var farmer = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == farmerId);

            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        // GET: Farmers/Edit
        // Displays form for farmers to edit their own profile information
        public async Task<IActionResult> Edit()
        {
            // Role-based authorization check - only farmers can edit their profile
            if (HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Index", "Home");
            }

            // Get the current farmer's ID from session
            int farmerId = int.Parse(HttpContext.Session.GetString("UserId"));

            // Retrieve the farmer's current information for editing
            var farmer = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == farmerId);

            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        // POST: Farmers/Edit
        // Processes the form submission to update a farmer's profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User farmer, string? NewPassword)
        {
            try
            {
                // Role-based authorization check - only farmers can edit their profile
                if (HttpContext.Session.GetString("Role") != "Farmer")
                {
                    TempData["ErrorMessage"] = "Only farmers can edit their profiles.";
                    return RedirectToAction("Index", "Home");
                }

                // Security check to ensure users can only edit their own profile
                int farmerId = int.Parse(HttpContext.Session.GetString("UserId"));
                if (farmer.UserId != farmerId)
                {
                    TempData["ErrorMessage"] = "You can only edit your own profile.";
                    return Forbid();
                }

                // Retrieve the existing farmer record from database
                var existingFarmer = await _context.Users
                    .FirstOrDefaultAsync(u => u.UserId == farmerId);

                if (existingFarmer == null)
                {
                    TempData["ErrorMessage"] = "User not found.";
                    return NotFound();
                }

                // Validate required fields to ensure data integrity
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

                // Check for username conflicts if it was changed
                if (farmer.Username != existingFarmer.Username &&
                    await _context.Users.AnyAsync(u => u.Username == farmer.Username))
                {
                    ModelState.AddModelError("Username", "This username is already taken.");
                }

                if (ModelState.IsValid)
                {
                    // Update the existing farmer's properties with new values
                    existingFarmer.FirstName = farmer.FirstName;
                    existingFarmer.LastName = farmer.LastName;
                    existingFarmer.Username = farmer.Username;
                    existingFarmer.Email = farmer.Email;
                    existingFarmer.FarmName = farmer.FarmName;
                    existingFarmer.FarmLocation = farmer.FarmLocation;
                    existingFarmer.FarmDescription = farmer.FarmDescription;

                    // Update password only if a new one is provided
                    if (!string.IsNullOrEmpty(NewPassword))
                    {
                        // Security: Hash the new password before storing
                        existingFarmer.Password = PasswordHelper.HashPassword(NewPassword);
                    }

                    // Save changes to database
                    _context.Update(existingFarmer);
                    await _context.SaveChangesAsync();

                    // Update session data to reflect the changes
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
                // Error handling with informative message
                TempData["ErrorMessage"] = $"An error occurred while updating your profile: {ex.Message}";
                return View(farmer);
            }
        }
    }
}