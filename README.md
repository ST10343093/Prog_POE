# 🌱 Agri-Energy Connect 🔋

<div align="center">
  
  ![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-7.0-blue?style=for-the-badge&logo=dotnet)
  ![C#](https://img.shields.io/badge/C%23-8.0-purple?style=for-the-badge&logo=csharp)
  ![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-red?style=for-the-badge&logo=microsoftsqlserver)
  ![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-blueviolet?style=for-the-badge&logo=bootstrap)
  
  <h3>Sustainable Agriculture Meets Green Energy</h3>
  
  <p>A modern platform connecting sustainable farmers with renewable energy solutions</p>
</div>

##  Overview

Agri-Energy Connect is a web-based platform designed to bridge the gap between sustainable agriculture and green energy solutions in South Africa. The platform connects farmers with energy experts, facilitating knowledge sharing, resource access, and collaboration opportunities to promote sustainable farming practices and renewable energy adoption.

##  Key Features

- **User Authentication**: Secure login and role-based access control
- **Farmer Profiles**: Detailed farmer information and farm details
- **Product Marketplace**: Platform for farmers to showcase their sustainable agricultural products
- **Responsive Design**: Mobile-friendly interface using Bootstrap 5
- **Data Management**: Entity Framework Core with SQL Server backend

##  User Roles

The platform supports two primary user roles:

1. **Employee**: System administrators who can:
   - Manage farmer accounts and profiles
   - View all products across the platform
   - Access farmer details and performance metrics
   
2. **Farmer**: Agricultural producers who can:
   - Manage their profile and farm information
   - Add and manage their product listings
   - View products from other farmers

##  Technology Stack

- **Framework**: ASP.NET Core 7.0 MVC
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap 5
- **Backend**: C#, Entity Framework Core
- **Database**: Microsoft SQL Server
- **Authentication**: Session-based authentication with password hashing
- **Styling**: Bootstrap Icons, Google Fonts (Montserrat, Poppins)

##  Prerequisites

Before you begin, ensure you have the following installed:
- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) (recommended) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express edition is sufficient for development)
- [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (or any SQL client)

##  Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/agri-energy-connect.git
cd agri-energy-connect
```

### 2. Database Configuration

1. Open SQL Server Management Studio or your preferred SQL client
2. Create a new database named `AgriEnergyConnectDB`
3. Update the connection string in `appsettings.json` to match your SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=AgriEnergyConnectDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

### 3. Apply Database Migrations

Run the following commands in the project directory to create the database schema:

Using Visual Studio:
- Open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console)
- Run: `Update-Database`

Using the command line:
```bash
dotnet ef database update
```

### 4. Build and Run the Application

#### Using Visual Studio:
1. Open the solution file (`Prog_POE.sln`) in Visual Studio
2. Build the solution (Ctrl+Shift+B)
3. Press F5 to run the application

#### Using the command line:
```bash
dotnet build
dotnet run
```

The application should now be running at `https://localhost:7087/` or `http://localhost:5099/`.

##  Using the Application

### Landing Page
- The landing page provides an overview of the platform's features and benefits
- Access the login page using the "Sign In" button

### Authentication
- Use the following demo credentials to test the application:
  - Employee: Username: `employee1`, Password: `password123`
  - Farmer: Username: `farmer1`, Password: `password123`

### Dashboard
- Upon successful login, users are directed to the home dashboard
- The dashboard displays key statistics and system features

### For Employees
1. **Managing Farmers**:
   - Navigate to the "Farmers" section to view the list of registered farmers
   - Add new farmers using the "Add New Farmer" button
   - View detailed farmer profiles by clicking on "Details"

2. **Viewing Products**:
   - Access the "All Products" section to browse products from all farmers
   - Filter products by category or date range
   - View detailed product information by clicking on a product

### For Farmers
1. **Managing Profile**:
   - Access your profile via the dropdown menu in the top-right corner
   - Edit your profile information using the "Edit Profile" button

2. **Managing Products**:
   - Navigate to "My Products" to view your current product listings
   - Add new products using the "Add New Product" button
   - View or delete existing products from this page

##  Development Workflow

### Project Structure

The application follows the standard ASP.NET Core MVC structure:

- **Controllers/**: Contains the application's controllers
  - `AuthController.cs`: Handles user authentication
  - `BaseController.cs`: Base controller with common functionality
  - `FarmersController.cs`: Manages farmer-related operations
  - `HomeController.cs`: Handles dashboard and home page
  - `LandingController.cs`: Controls the landing page
  - `ProductsController.cs`: Manages product operations

- **Models/**: Contains the application's data models
  - `User.cs`: Represents users (employees and farmers)
  - `Product.cs`: Represents agricultural products
  - `ErrorViewModel.cs`: Used for error handling

- **Views/**: Contains the application's views, organized by controller
  - `Auth/`: Login and registration views
  - `Farmers/`: Farmer management views
  - `Home/`: Dashboard views
  - `Landing/`: Landing page views
  - `Products/`: Product management views
  - `Shared/`: Layouts and shared components

- **Data/**: Contains database context and configurations
  - `ApplicationDbContext.cs`: Entity Framework database context

- **wwwroot/**: Contains static files
  - `css/`: Stylesheet files
  - `js/`: JavaScript files
  - `images/`: Image assets
  - `lib/`: Client-side libraries

### Extending the Application

1. **Adding New Features**:
   - Create new controllers in the `Controllers/` directory
   - Add corresponding views in the `Views/` directory
   - Update navigation in `_Layout.cshtml`

2. **Modifying Database Schema**:
   - Update model classes in the `Models/` directory
   - Create a new migration using Entity Framework:
     - `Add-Migration MigrationName` (Visual Studio)
     - `dotnet ef migrations add MigrationName` (command line)
   - Apply the migration to the database:
     - `Update-Database` (Visual Studio)
     - `dotnet ef database update` (command line)

##  Testing

The application includes sample data to facilitate testing:

- **Sample Users**:
  - Employee: Username: `employee1`, Password: `password123`
  - Farmers: Username: `farmer1`, Password: `password123`, Username: `farmer2`, Password: `password123`

- **Sample Products**:
  - Organic Tomatoes
  - Free-Range Eggs
  - Grass-Fed Beef
  - Honey
  - Organic Spinach

##  License

This project is licensed under the MIT License - see the LICENSE file for details.

##  Contact

For questions or support, please contact:
- Email: arshadbhula547@gmail.com
- GitHub: [Your GitHub Profile]([https://github.com/yourusername](https://github.com/ST10343093/Prog_POE))

---

Thank you for using Agri-Energy Connect! We hope this platform contributes to more sustainable farming practices and broader adoption of renewable energy solutions in agriculture.
