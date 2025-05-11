using Microsoft.EntityFrameworkCore;
using Prog_POE.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Prog_POE.Data
{
    // Database context class that serves as the main entry point for database interactions
    // Manages entity sets and defines the relationship between entity classes and database tables
    public class ApplicationDbContext : DbContext
    {
        // Constructor that accepts database configuration options from dependency injection
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties representing database tables
        // Each DbSet allows CRUD operations on the corresponding entities
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        // Method that configures the database model and relationships
        // Called when the model is being created during startup
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for users - provides initial employee and farmer accounts
            // This ensures the application has data to work with from the first run
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "employee1",
                    Password = "password123", // In a real app, use password hashing
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "john.smith@agrienergy.com",
                    Role = "Employee"
                },
                new User
                {
                    UserId = 2,
                    Username = "farmer1",
                    Password = "password123", // In a real app, use password hashing
                    FirstName = "Sarah",
                    LastName = "Johnson",
                    Email = "sarah@greenfarms.co.za",
                    Role = "Farmer",
                    FarmName = "Green Valley Farms",
                    FarmLocation = "Western Cape",
                    FarmDescription = "Sustainable organic farm specializing in vegetables and fruits."
                },
                new User
                {
                    UserId = 3,
                    Username = "farmer2",
                    Password = "password123", // In a real app, use password hashing
                    FirstName = "David",
                    LastName = "Nkosi",
                    Email = "david@sunrisefarm.co.za",
                    Role = "Farmer",
                    FarmName = "Sunrise Eco Farm",
                    FarmLocation = "KwaZulu-Natal",
                    FarmDescription = "Family-owned farm focusing on sustainable livestock and dairy production."
                }
            );

            // Seed data for products - provides initial product catalog
            // Demonstrates various product types and attributes that can be listed
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Organic Tomatoes",
                    Category = "Vegetables",
                    ProductionDate = new DateTime(2025, 3, 15),
                    Description = "Fresh organic tomatoes grown without pesticides",
                    Price = 25.99m,
                    IsOrganic = true,
                    ImageUrl = "/images/products/tomatoes.jpg",
                    FarmerId = 2  // Links to Sarah Johnson's farm
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Free-Range Eggs",
                    Category = "Dairy",
                    ProductionDate = new DateTime(2025, 4, 1),
                    Description = "Farm fresh eggs from free-range chickens",
                    Price = 45.50m,
                    IsOrganic = true,
                    ImageUrl = "/images/products/eggs.jpg",
                    FarmerId = 2  // Links to Sarah Johnson's farm
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Grass-Fed Beef",
                    Category = "Meat",
                    ProductionDate = new DateTime(2025, 3, 20),
                    Description = "Premium grass-fed beef from ethically raised cattle",
                    Price = 180.00m,
                    IsOrganic = true,
                    ImageUrl = "/images/products/beef.jpg",
                    FarmerId = 3  // Links to David Nkosi's farm
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Honey",
                    Category = "Sweeteners",
                    ProductionDate = new DateTime(2025, 2, 10),
                    Description = "Raw, unfiltered honey from local beehives",
                    Price = 85.75m,
                    IsOrganic = true,
                    ImageUrl = "/images/products/honey.jpg",
                    FarmerId = 3  // Links to David Nkosi's farm
                },
                new Product
                {
                    ProductId = 5,
                    Name = "Organic Spinach",
                    Category = "Vegetables",
                    ProductionDate = new DateTime(2025, 4, 5),
                    Description = "Fresh organic spinach leaves",
                    Price = 18.99m,
                    IsOrganic = true,
                    ImageUrl = "/images/products/spinach.jpg",
                    FarmerId = 2  // Links to Sarah Johnson's farm
                }
            );
        }
    }
}