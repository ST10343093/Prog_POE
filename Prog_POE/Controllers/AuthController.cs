using Microsoft.AspNetCore.Mvc;
using Prog_POE.Data;
using Prog_POE.Models;
using Microsoft.EntityFrameworkCore;
using Prog_POE.Helpers;

namespace Prog_POE.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            // Check if user is already logged in
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
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

            // Debug info
            System.Diagnostics.Debug.WriteLine($"Username: {username}");
            System.Diagnostics.Debug.WriteLine($"Stored password: {user.Password}");
            System.Diagnostics.Debug.WriteLine($"Contains colon: {user.Password.Contains(':')}");

            // Check if we need to handle legacy passwords (not hashed)
            if (!user.Password.Contains(':'))
            {
                // Legacy password - direct comparison
                if (user.Password != password)
                {
                    ViewBag.Error = "Invalid username or password.";
                    return View();
                }

                // Update the user's password to a hashed version
                user.Password = PasswordHelper.HashPassword(password);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Hashed password - verify using the hashing function
                if (!PasswordHelper.VerifyPassword(user.Password, password))
                {
                    ViewBag.Error = "Invalid username or password.";
                    return View();
                }
            }

            // Store user info in session
            HttpContext.Session.SetString("UserId", user.UserId.ToString());
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);
            HttpContext.Session.SetString("FullName", $"{user.FirstName} {user.LastName}");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // GET: Auth/Register
        public IActionResult Register()
        {
            // Check if user is already logged in
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: Auth/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                ModelState.AddModelError("Username", "This username is already taken.");
                return View(user);
            }

            // Set role to Farmer (by default, new registrations are for farmers)
            user.Role = "Farmer";

            if (ModelState.IsValid)
            {
                // Hash the password before storing it
                user.Password = PasswordHelper.HashPassword(user.Password);

                _context.Add(user);
                await _context.SaveChangesAsync();

                // Log the user in automatically
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);
                HttpContext.Session.SetString("FullName", $"{user.FirstName} {user.LastName}");

                TempData["SuccessMessage"] = "Registration successful! Welcome to Agri-Energy Connect.";
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
    }
}