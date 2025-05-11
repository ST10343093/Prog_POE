using Microsoft.AspNetCore.Mvc;
using Prog_POE.Data;
using Prog_POE.Models;
using Microsoft.EntityFrameworkCore;
using Prog_POE.Helpers;

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



    // Controls all authentication-related operations including login, logout, and registration
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor that injects the database context dependency
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Displays the login page if user is not already authenticated
        public IActionResult Login()
        {
            // Check if user is already logged in
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Processes the login form submission with username and password validation
        // Supports both legacy plain text passwords and new hashed passwords
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Validate that required fields are provided
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Username and password are required.";
                return View();
            }

            // Get user by username only (we'll verify password separately)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            // Debug information for troubleshooting authentication issues
            System.Diagnostics.Debug.WriteLine($"Username: {username}");
            System.Diagnostics.Debug.WriteLine($"Stored password: {user.Password}");
            System.Diagnostics.Debug.WriteLine($"Contains colon: {user.Password.Contains(':')}");

            // Password migration logic: handle both old plain text and new hashed passwords
            if (!user.Password.Contains(':'))
            {
                // Legacy password - direct comparison
                if (user.Password != password)
                {
                    ViewBag.Error = "Invalid username or password.";
                    return View();
                }

                // Upgrade security: Update the user's password to a hashed version
                user.Password = PasswordHelper.HashPassword(password);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Hashed password - verify using the secure hashing function
                if (!PasswordHelper.VerifyPassword(user.Password, password))
                {
                    ViewBag.Error = "Invalid username or password.";
                    return View();
                }
            }

            // Store essential user information in session for authentication and personalization
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("FullName", $"{user.FirstName} {user.LastName}");

            return RedirectToAction("Index", "Home");
        }

        // Logs the current user out by clearing all session data
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Displays the registration form for new users
        public IActionResult Register()
        {
            // Prevent authenticated users from accessing registration
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // Processes the user registration form submission
        // Creates new user accounts with proper password hashing
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            // Ensure username uniqueness
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                ModelState.AddModelError("Username", "This username is already taken.");
                return View(user);
            }

            // Set role to Farmer (by default, new registrations are for farmers)
            user.Role = "Farmer";

            if (ModelState.IsValid)
            {
                // Security: Hash the password before storing it in the database
                user.Password = PasswordHelper.HashPassword(user.Password);

                // Save the new user to the database
                _context.Add(user);
                await _context.SaveChangesAsync();

                // Automatically log in the newly registered user
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);
                HttpContext.Session.SetString("FullName", $"{user.FirstName} {user.LastName}");

                TempData["SuccessMessage"] = "Registration successful! Welcome to Agri-Energy Connect.";
                return RedirectToAction("Index", "Home");
            }

            // Return to form with validation errors if model state is invalid
            return View(user);
        }
    }
}