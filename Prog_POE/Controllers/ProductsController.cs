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
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Farmer)
                .ToListAsync();

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
            // Check if user is a farmer
            if (HttpContext.Session.GetString("Role") != "Farmer")
            {
                return RedirectToAction("Index", "Home");
            }

            // Set farmer ID from session
            product.FarmerId = int.Parse(HttpContext.Session.GetString("UserId"));

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Product added successfully!";
                return RedirectToAction(nameof(MyProducts));
            }
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
    }
}