using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prog_POE.Data;
using Prog_POE.Models;

namespace Prog_POE.Controllers
{
    // Controller for managing product-related operations
    // Inherits from BaseController for authentication requirements
    public class ProductsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        // Constructor with database context injection
        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        // Displays all products with optional filtering capabilities
        public async Task<IActionResult> Index(string category = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            // Start with all products and include related farmer details
            var query = _context.Products.Include(p => p.Farmer).AsQueryable();

            // Apply filters if provided to narrow down results
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }

            if (startDate.HasValue)
            {
                query = query.Where(p => p.ProductionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(p => p.ProductionDate <= endDate.Value);
            }

            // Prepare filter dropdown options with unique categories
            var categories = await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            // Pass filter values to view for maintaining state
            ViewBag.Categories = new SelectList(categories);
            ViewBag.SelectedCategory = category;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            // Execute query and return results
            var products = await query.ToListAsync();

            return View(products);
        }

        // GET: Products/FarmerProducts/5
        // Shows products from a specific farmer with filtering options
        public async Task<IActionResult> FarmerProducts(int? id, string category = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Verify the farmer exists and is actually a farmer
            var farmer = await _context.Users
                .FirstOrDefaultAsync(f => f.UserId == id && f.Role == "Farmer");

            if (farmer == null)
            {
                return NotFound();
            }

            // Pass farmer info to view for display
            ViewBag.Farmer = farmer;

            // Start with all products from this specific farmer
            var query = _context.Products
                .Where(p => p.FarmerId == id);

            // Apply additional filters if provided
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }

            if (startDate.HasValue)
            {
                query = query.Where(p => p.ProductionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(p => p.ProductionDate <= endDate.Value);
            }

            // Get categories specific to this farmer's products for the filter
            var categories = await _context.Products
                .Where(p => p.FarmerId == id)
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            // Pass filter values to view
            ViewBag.Categories = new SelectList(categories);
            ViewBag.SelectedCategory = category;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            // Execute query and return results
            var products = await query.ToListAsync();

            return View(products);
        }

        // GET: Products/MyProducts
        // For farmers to view and manage their own products
        public async Task<IActionResult> MyProducts()
        {
            // Role-based authorization check - only farmers can access this
            if (HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Index", "Home");
            }

            // Get the current user's ID from session
            int farmerId = int.Parse(HttpContext.Session.GetString("UserId"));

            // Retrieve all products belonging to this farmer
            var products = await _context.Products
                .Where(p => p.FarmerId == farmerId)
                .ToListAsync();

            return View(products);
        }

        // GET: Products/Create
        // Displays form for farmers to add new products
        public IActionResult Create()
        {
            // Role-based authorization check - only farmers can add products
            if (HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Products/Create
        // Processes form submission to add a new product
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                // Role-based authorization check - only farmers can add products
                if (HttpContext.Session.GetString("Role") != "Farmer")
                {
                    return RedirectToAction("Index", "Home");
                }

                // Associate product with the current farmer
                product.FarmerId = int.Parse(HttpContext.Session.GetString("UserId"));

                // Process and standardize image URL format
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    // External URL handling - keep as is if it's a web URL
                    if (!product.ImageUrl.StartsWith("http://") && !product.ImageUrl.StartsWith("https://"))
                    {
                        // Local file path handling - normalize path format
                        product.ImageUrl = product.ImageUrl.Replace("\\", "/").TrimStart('/');
                        product.ImageUrl = "/images/products/" + product.ImageUrl;
                    }
                }

                // Save the new product to database
                _context.Add(product);
                await _context.SaveChangesAsync();

                // Success feedback and redirect
                TempData["SuccessMessage"] = "Product added successfully!";
                return RedirectToAction(nameof(MyProducts));
            }
            catch (Exception ex)
            {
                // Error handling with detailed feedback
                ModelState.AddModelError("", $"Error creating product: {ex.Message}");

                // Diagnostic logging for troubleshooting
                System.Diagnostics.Debug.WriteLine($"Error in ProductsController.Create: {ex}");
            }

            // If an error occurred, redisplay the form with validation messages
            return View(product);
        }

        // GET: Products/Details/5
        // Displays detailed information about a specific product
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve product with related farmer information
            var product = await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Delete/5
        // Displays confirmation page before deleting a product
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Get product with farmer details for display
            var product = await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Security check - ensure only the product owner or admin can delete
            if (HttpContext.Session.GetString("Role") == "Farmer" &&
                int.Parse(HttpContext.Session.GetString("UserId")) != product.FarmerId)
            {
                return Forbid();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        // Processes the product deletion after confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Find the product to delete
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                // Security check - ensure only the product owner or admin can delete
                if (HttpContext.Session.GetString("Role") == "Farmer" &&
                    int.Parse(HttpContext.Session.GetString("UserId")) != product.FarmerId)
                {
                    return Forbid();
                }

                // Remove the product from database
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                // Success feedback
                TempData["SuccessMessage"] = "Product deleted successfully!";
            }
            catch (Exception ex)
            {
                // Error handling with feedback
                TempData["ErrorMessage"] = $"Error deleting product: {ex.Message}";
            }

            // Redirect based on user role - farmers go to their products, others to all products
            if (HttpContext.Session.GetString("Role") == "Farmer")
            {
                return RedirectToAction(nameof(MyProducts));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}