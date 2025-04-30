using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prog_POE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FarmName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FarmLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FarmDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsOrganic = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FarmerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Users_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FarmDescription", "FarmLocation", "FarmName", "FirstName", "LastName", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "john.smith@agrienergy.com", null, null, null, "John", "Smith", "password123", "Employee", "employee1" },
                    { 2, "sarah@greenfarms.co.za", "Sustainable organic farm specializing in vegetables and fruits.", "Western Cape", "Green Valley Farms", "Sarah", "Johnson", "password123", "Farmer", "farmer1" },
                    { 3, "david@sunrisefarm.co.za", "Family-owned farm focusing on sustainable livestock and dairy production.", "KwaZulu-Natal", "Sunrise Eco Farm", "David", "Nkosi", "password123", "Farmer", "farmer2" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Category", "Description", "FarmerId", "ImageUrl", "IsOrganic", "Name", "Price", "ProductionDate" },
                values: new object[,]
                {
                    { 1, "Vegetables", "Fresh organic tomatoes grown without pesticides", 2, "/images/products/tomatoes.jpg", true, "Organic Tomatoes", 25.99m, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Dairy", "Farm fresh eggs from free-range chickens", 2, "/images/products/eggs.jpg", true, "Free-Range Eggs", 45.50m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Meat", "Premium grass-fed beef from ethically raised cattle", 3, "/images/products/beef.jpg", true, "Grass-Fed Beef", 180.00m, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Sweeteners", "Raw, unfiltered honey from local beehives", 3, "/images/products/honey.jpg", true, "Honey", 85.75m, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "Vegetables", "Fresh organic spinach leaves", 2, "/images/products/spinach.jpg", true, "Organic Spinach", 18.99m, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_FarmerId",
                table: "Products",
                column: "FarmerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
