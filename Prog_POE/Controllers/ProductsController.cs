using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prog_POE.Data;
using Prog_POE.Models;

namespace Prog_POE.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string category = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            // Start with all products
            var query = _context.Products.Include(p => p.Farmer).AsQueryable();

            // Apply filters if provided
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

            // Get distinct categories for the filter dropdown
            var categories = await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            ViewBag.Categories = new SelectList(categories);
            ViewBag.SelectedCategory = category;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            var products = await query.ToListAsync();

            return View(products);
        }

        // GET: Products/FarmerProducts/5
        public async Task<IActionResult> FarmerProducts(int? id, string category = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await _context.Users
                .FirstOrDefaultAsync(f => f.UserId == id && f.Role == "Farmer");

            if (farmer == null)
            {
                return NotFound();
            }

            ViewBag.Farmer = farmer;

            // Get all products from this farmer
            var query = _context.Products
                .Where(p => p.FarmerId == id);

            // Apply filters if provided
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

            // Get distinct categories for the filter dropdown
            var categories = await _context.Products
                .Where(p => p.FarmerId == id)
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            ViewBag.Categories = new SelectList(categories);
            ViewBag.SelectedCategory = category;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            var products = await query.ToListAsync();

            return View(products);
        }

        // GET: Products/MyProducts - For farmers to see their own products
        public async Task<IActionResult> MyProducts()
        {
            // Check if user is a farmer
            if (HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Index", "Home");
            }

            int farmerId = int.Parse(HttpContext.Session.GetString("UserId"));

            var products = await _context.Products
                .Where(p => p.FarmerId == farmerId)
                .ToListAsync();

            return View(products);
        }

        // GET: Products/Create - For farmers to add new products
        public IActionResult Create()
        {
            // Check if user is a farmer
            if (HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                // Check if user is a farmer
                if (HttpContext.Session.GetString("Role") != "Farmer")
                {
                    return RedirectToAction("Index", "Home");
                }

                // Set farmer ID from session
                product.FarmerId = int.Parse(HttpContext.Session.GetString("UserId"));

                // Handle image URL
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    // Check if it's an internet URL (starts with http:// or https://)
                    if (!product.ImageUrl.StartsWith("http://") && !product.ImageUrl.StartsWith("https://"))
                    {
                        // It's a local file - ensure it has the correct format
                        product.ImageUrl = product.ImageUrl.Replace("\\", "/").TrimStart('/');
                        product.ImageUrl = "/images/products/" + product.ImageUrl;
                    }
                }

                // Add the product to the database
                _context.Add(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Product added successfully!";
                return RedirectToAction(nameof(MyProducts));
            }
            catch (Exception ex)
            {
                // Add detailed error message
                ModelState.AddModelError("", $"Error creating product: {ex.Message}");

                // Log the error (you can replace this with proper logging)
                System.Diagnostics.Debug.WriteLine($"Error in ProductsController.Create: {ex}");
            }

            // If we get here, something failed - redisplay the form
            return View(product);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Farmer)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Check if the current user is the owner of this product or an employee
            if (HttpContext.Session.GetString("Role") == "Farmer" &&
                int.Parse(HttpContext.Session.GetString("UserId")) != product.FarmerId)
            {
                return Forbid();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                // Check if the current user is the owner of this product or an employee
                if (HttpContext.Session.GetString("Role") == "Farmer" &&
                    int.Parse(HttpContext.Session.GetString("UserId")) != product.FarmerId)
                {
                    return Forbid();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Product deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting product: {ex.Message}";
            }

            // Redirect back to the appropriate product list
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