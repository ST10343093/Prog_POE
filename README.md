# 🌱 Agri-Energy Connect 🔋

<div align="center">
  
  ![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-9.0-blue?style=for-the-badge&logo=dotnet)
  ![C#](https://img.shields.io/badge/C%23-8.0-purple?style=for-the-badge&logo=csharp)
  ![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-red?style=for-the-badge&logo=microsoftsqlserver)
  ![Bootstrap](https://img.shields.io/badge/Bootstrap-5.0-blueviolet?style=for-the-badge&logo=bootstrap)
  
  <h3>Sustainable Agriculture Meets Green Energy</h3>
  
  <p>A modern platform connecting sustainable farmers with renewable energy solutions</p>
  
  <a href="https://youtu.be/0Pf9or5f0w4">
    <img src="https://img.shields.io/badge/Watch_Demo-YouTube-red?style=for-the-badge&logo=youtube" alt="Watch Demo on YouTube">
  </a>
</div>

## Overview

Agri-Energy Connect is a web-based platform designed to bridge the gap between sustainable agriculture and green energy solutions in South Africa. The platform connects farmers with energy experts, facilitating knowledge sharing, resource access, and collaboration opportunities to promote sustainable farming practices and renewable energy adoption.

## Key Features

- **User Authentication**: Secure login and role-based access control
- **Farmer Profiles**: Detailed farmer information and farm details
- **Product Marketplace**: Platform for farmers to showcase their sustainable agricultural products
- **Responsive Design**: Mobile-friendly interface using Bootstrap 5
- **Data Management**: Entity Framework Core with SQL Server backend

## User Roles

The platform supports two primary user roles:

1. **Employee**: System administrators who can:
   - Manage farmer accounts and profiles
   - View all products across the platform
   - Access farmer details and performance metrics
   
2. **Farmer**: Agricultural producers who can:
   - Manage their profile and farm information
   - Add and manage their product listings
   - View products from other farmers

## Technology Stack

- **Framework**: ASP.NET Core 9.0 MVC
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap 5
- **Backend**: C#, Entity Framework Core
- **Database**: Microsoft SQL Server
- **Authentication**: Session-based authentication with password hashing
- **Styling**: Bootstrap Icons, Google Fonts (Montserrat, Poppins)

## Prerequisites

Before you begin, ensure you have the following installed:

### Essential Software Requirements
- **.NET 9.0 SDK**: Required to build and run the application. Download from the [official .NET website](https://dotnet.microsoft.com/download/dotnet/9.0).
- **Visual Studio 2022**: Recommended IDE for development. Download from the [Visual Studio website](https://visualstudio.microsoft.com/downloads/). 
  - During installation, ensure you select the "ASP.NET and web development" workload.
  - For .NET 9.0 development, make sure to include the necessary components during installation.
  - Alternatively, you can use Visual Studio Code with the C# extension.
- **SQL Server**: Required for the database backend. Options include:
  - SQL Server 2019 Express (free, recommended for development)
  - SQL Server Developer Edition
  - SQL Server Standard/Enterprise Edition
- **SQL Server Management Studio (SSMS)**: Recommended tool for managing SQL Server databases.
  - Alternative SQL clients: Azure Data Studio, DBeaver, or any SQL client of your choice.

### Additional Requirements
- **Git**: For version control and cloning the repository.
- **Entity Framework Core Tools**: For database migrations, can be installed via NuGet package manager in Visual Studio or via the command line:
  ```bash
  dotnet tool install --global dotnet-ef
  ```
- **Internet Connection**: Required for downloading package dependencies and NuGet packages.

## Setup Instructions

### 1. Clone the Repository

Open a command prompt or terminal and execute the following commands:

```bash
git clone https://github.com/ST10343093/Prog_POE
cd Prog_POE
```

### 2. Database Configuration

Follow these steps to set up your database:

#### Step 2.1: Create the Database
1. Open SQL Server Management Studio (SSMS)
2. Connect to your SQL Server instance
3. Right-click on "Databases" in the Object Explorer
4. Select "New Database..."
5. Enter `AgriEnergyConnectDB` as the database name
6. Click "OK" to create the database

#### Step 2.2: Update Connection String
1. Open the `appsettings.json` file in the project root directory
2. Locate the `ConnectionStrings` section
3. Update the `DefaultConnection` to match your SQL Server instance name:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YourServerName\\SQLEXPRESS;Database=AgriEnergyConnectDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

Replace `YourServerName` with your actual SQL Server instance name. You can find your server name in SSMS when you connect to the database.

**Note for SQL Server Authentication**: If you're using SQL Server Authentication instead of Windows Authentication, your connection string should be:

```
"DefaultConnection": "Server=YourServerName\\SQLEXPRESS;Database=AgriEnergyConnectDB;User Id=YourUsername;Password=YourPassword;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

### 3. Apply Database Migrations

This step will create all necessary database tables and seed initial data:

#### Option A: Using Visual Studio Package Manager Console
1. Open the solution file (`Prog_POE.sln`) in Visual Studio
2. Open the Package Manager Console from the menu: Tools > NuGet Package Manager > Package Manager Console
3. Ensure the Default project (dropdown at top of console) is set to the main project
4. In the console, execute the following command:
   ```
   Update-Database
   ```
5. Wait for the command to complete - you should see "Done." message

#### Option B: Using Command Line Interface (CLI)
1. Navigate to the project directory in your terminal
2. Make sure you have the EF Core tools installed (see Prerequisites)
3. Execute the following command:
   ```bash
   dotnet ef database update
   ```
4. Wait for the command to complete successfully

### 4. Install Required NuGet Packages

The project depends on several NuGet packages. If they aren't automatically restored, you'll need to install them manually:

#### Using Visual Studio:
1. Right-click on the solution in Solution Explorer
2. Select "Restore NuGet Packages"
3. Wait for the process to complete
4. If any package errors occur, open the NuGet Package Manager (Tools > NuGet Package Manager > Manage NuGet Packages for Solution)
5. Install any missing packages, ensuring versions are compatible with .NET 9.0

#### Using Command Line:
```bash
dotnet restore
```

### 5. Configure IIS Express (Optional)

If you're using IIS Express in Visual Studio:

1. Right-click on the project in Solution Explorer
2. Select "Properties"
3. Go to the "Debug" tab
4. Ensure the profile is set to "IIS Express"
5. Verify the App URL is set correctly (typically https://localhost:44300/ or similar)

### 6. Build and Run the Application

#### Option A: Using Visual Studio
1. Open the solution file (`Prog_POE.sln`) in Visual Studio
2. Build the solution by pressing Ctrl+Shift+B or selecting Build > Build Solution from the menu
3. If any build errors occur, resolve them before proceeding
4. Run the application by pressing F5 or clicking the green "Play" button
5. Wait for the application to compile and launch
6. Your default browser should open automatically with the application running

#### Option B: Using Command Line
```bash
dotnet build
dotnet run
```

The application should now be running at `https://localhost:7087/` or `http://localhost:5099/`, depending on your configuration.

### 7. Verify Database Setup

To ensure your database has been correctly set up with seed data:

1. Open SQL Server Management Studio
2. Connect to your database server
3. Expand the `AgriEnergyConnectDB` database
4. Expand the "Tables" folder
5. Right-click on any table (e.g., "Users" or "Products") and select "Select Top 1000 Rows"
6. Verify that the seed data is present in the tables

If the tables are empty or don't exist:
1. Check the migration history table (`__EFMigrationsHistory`)
2. Ensure all migrations have been applied
3. If issues persist, try repeating Step 3 with the `-verbose` flag

## Using the Application

### Landing Page
- The landing page provides an overview of the platform's features and benefits
- Access the login page using the "Sign In" button in the top-right corner
- The landing page showcases testimonials, featured products, and the platform's value proposition

### Authentication

#### Login Process
1. Navigate to the Login page by clicking "Sign In" on the landing page
2. Enter your username and password in the respective fields
3. Click the "Sign In" button to authenticate
4. If successful, you'll be redirected to the dashboard
5. If unsuccessful, an error message will display with the reason

#### Registration Process (for Farmers)
1. Click "Register" on the landing page or login page
2. Fill in the required personal information:
   - First Name and Last Name
   - Email Address (must be valid format)
   - Username (must be unique)
   - Password
3. Provide farm information:
   - Farm Name
   - Farm Location
   - Farm Description detailing sustainable practices
4. Click "Create Account" to register
5. Upon successful registration, you'll be automatically logged in and directed to the dashboard

#### Demo Credentials
For testing purposes, you can use the following pre-configured accounts:

- **Employee Login**:
  - Username: `employee1`
  - Password: `password123`

- **Farmer Login**:
  - Username: `farmer1`
  - Password: `password123`
  - OR
  - Username: `farmer2`
  - Password: `password123`

### Dashboard Navigation

After logging in, the dashboard provides a central hub with:
- Key statistics (number of farmers, products, organic products)
- Quick access links to primary features
- Information about the platform's mission and benefits

#### Navigation Bar
- The navigation bar at the top provides access to different sections of the application
- Menu options vary based on user role (Employee vs. Farmer)
- Your username appears in the top-right with a dropdown menu for profile and logout options

### For Employees

Employees have access to administrative features to manage farmers and view all products.

#### 1. Managing Farmers
1. **View Farmers**:
   - Click "Farmers" in the navigation bar to see the list of all registered farmers
   - The list displays farmer names, farm names, locations, and email addresses
   - Each farmer entry has action buttons for viewing details and products

2. **Add New Farmer**:
   - Click the "Add New Farmer" button on the farmers list page
   - Complete the form with all required fields:
     - Personal Information (First Name, Last Name, etc.)
     - Login Credentials (Username, Password)
     - Farm Details (Farm Name, Location, Description)
   - Click "Create Farmer" to add the new account
   - You will be redirected to the farmers list with a success message

3. **View Farmer Details**:
   - Click "Details" on a farmer's row to view their complete profile
   - The details page shows personal information, farm details, and a list of their products
   - Click "View Products" to see all products from this specific farmer

#### 2. Viewing Products
1. **Browse All Products**:
   - Click "All Products" in the navigation bar
   - Products are displayed in a grid with images, names, prices, and organic status
   - Each product card shows essential information at a glance
   - Click on any product to view its detailed information

2. **Filter Products by Farmer**:
   - Go to a farmer's detail page
   - Click "View Products" to see only that farmer's products
   - Use the filter options to refine by category or date range
   - Apply filters by clicking the "Apply Filters" button
   - Clear filters by clicking "Clear Filters"

3. **View Product Details**:
   - Click on a product to access its detailed information page
   - See comprehensive information including description, price, production date, etc.
   - View information about the farmer who supplies the product
   - Return to the product list by clicking "Back to Products"

### For Farmers

Farmers can manage their profile and product listings.

#### 1. Managing Products
1. **View My Products**:
   - Click "My Products" in the navigation bar
   - See all your listed products with images, names, and prices
   - Products are displayed in a grid format for easy scanning
   - The system shows a message if you haven't added any products yet

2. **Add New Product**:
   - Click "Add New Product" button on the My Products page
   - Fill in the product details:
     - Name and Category (select from dropdown)
     - Production Date (using the date picker)
     - Price (in ZAR)
     - Description (detailed information about the product)
     - Organic status (checkbox)
     - Image URL (optional - for product image)
   - Click "Create Product" to add the listing
   - You will be redirected to your products list with a success message

3. **View Product Details**:
   - Click on any of your products to view its complete details
   - The system shows how customers will see your product listing
   - Return to your products by clicking "Back to My Products"

#### 2. Viewing Other Products
1. **Browse All Products**:
   - Click "All Products" in the navigation bar
   - View products from all farmers on the platform
   - This helps understand market offerings and competitive positioning

## Database Schema

The application uses two primary database tables:

### Users Table
- **UserId**: Primary key, auto-increment integer
- **Username**: Unique username for login (string, required)
- **Password**: User password (string, required)
- **FirstName**: User's first name (string, required)
- **LastName**: User's last name (string, required)
- **Email**: User's email address (string, required)
- **Role**: User role ("Employee" or "Farmer") (string, required)
- **FarmName**: Name of the farm (string, nullable)
- **FarmLocation**: Farm location (string, nullable)
- **FarmDescription**: Description of farm and practices (string, nullable)

### Products Table
- **ProductId**: Primary key, auto-increment integer
- **Name**: Product name (string, required)
- **Category**: Product category (string, required)
- **ProductionDate**: Date when the product was produced (DateTime, required)
- **Description**: Detailed product description (string, nullable)
- **Price**: Product price in ZAR (decimal, nullable)
- **IsOrganic**: Boolean indicating if the product is organic (bool)
- **ImageUrl**: URL to the product image (string, nullable)
- **FarmerId**: Foreign key to Users table (integer, required)

The relationship between Products and Users is a many-to-one relationship, where a User (Farmer) can have many Products.

## Troubleshooting Common Issues

### Connection String Problems
- **Issue**: "Cannot connect to database" error
- **Solution**: 
  1. Verify your SQL Server instance name is correct in the connection string
  2. Ensure SQL Server is running (check in Services or SQL Server Configuration Manager)
  3. Check that the database exists
  4. Verify your Windows account has permission to access the database, or that the SQL credentials are correct
  5. Make sure the format of the connection string is correct, especially backslashes and quotes

### Migration Errors
- **Issue**: "No migrations applied" or "Pending migrations" warnings
- **Solution**:
  1. Run `Update-Database` in Package Manager Console
  2. Verify the migrations folder exists and contains migration files
  3. Check for errors in the migration files
  4. If problems persist, try `Add-Migration InitialCreate -Force` to recreate the initial migration
  5. For severe issues, you might need to delete the database and start fresh

### Runtime Errors
- **Issue**: Application crashes or displays error page
- **Solution**:
  1. Check the application logs (in the console or log files)
  2. Verify all NuGet packages are correctly installed and compatible with .NET 9.0
  3. Ensure the database is properly configured and accessible
  4. Check for syntax errors in your code
  5. Try running the application in Debug mode to catch exceptions

### Login Issues
- **Issue**: Cannot log in with provided credentials
- **Solution**:
  1. Verify the username and password are correct (case-sensitive)
  2. Check that the database contains the user record (query the Users table)
  3. Clear browser cookies and try again
  4. Check for any validation errors in the login form
  5. If using a new account, verify it was successfully created in the database

### Page Not Found Errors
- **Issue**: 404 errors when navigating to certain pages
- **Solution**:
  1. Check route configurations in Program.cs
  2. Verify controller and action method names match the URL
  3. Check for typos in URLs or link href attributes
  4. Ensure controllers inherit from the correct base class

## Extending the Application

### Adding New Features

1. **Create New Models**:
   - Add new class files to the `Models` folder
   - Define properties and relationships
   - Add appropriate data annotations for validation
   - Update `ApplicationDbContext.cs` to include new DbSet properties

2. **Create Database Migrations**:
   - After adding/modifying models, create a migration:
     - Using Package Manager Console: `Add-Migration MigrationName`
     - Using CLI: `dotnet ef migrations add MigrationName`
   - Apply the migration:
     - Using Package Manager Console: `Update-Database`
     - Using CLI: `dotnet ef database update`
   - Verify the database schema changes in SSMS

3. **Add Controllers**:
   - Create new controller classes in the `Controllers` folder
   - Inherit from `BaseController` to maintain authentication requirements
   - Implement action methods for CRUD operations
   - Follow RESTful naming conventions for consistency

4. **Create Views**:
   - Add new views in the `Views` folder, organized by controller
   - Use the existing layout templates for consistency
   - Implement form validation and error handling
   - Maintain responsive design principles with Bootstrap 5

5. **Update Navigation**:
   - Modify `_Layout.cshtml` to include links to new features
   - Consider role-based access for new functionality
   - Update dropdown menus or sidebar as needed

### Modifying Existing Features

1. **Updating Views**:
   - Locate the view file in the relevant controller's Views folder
   - Make changes to the HTML/Razor markup
   - Test changes thoroughly for responsiveness and functionality
   - Consider impact on different user roles

2. **Modifying Controllers**:
   - Find the controller class in the Controllers folder
   - Update or add action methods as needed
   - Ensure proper validation and error handling
   - Maintain security by checking user roles

3. **Changing Data Models**:
   - Update the model class in the Models folder
   - Create a new migration to apply changes to the database
   - Test thoroughly to ensure data integrity

## Development Best Practices

1. **Code Organization**:
   - Follow the established MVC pattern
   - Keep controllers focused on specific functionality
   - Use view models for complex view data requirements
   - Organize code in logical namespaces

2. **Security Considerations**:
   - Implement proper input validation
   - Use parameterized queries to prevent SQL injection
   - Consider implementing password hashing for production
   - Always validate user roles for authorization
   - Never trust client-side data

3. **Styling Guidelines**:
   - Maintain consistent use of Bootstrap classes
   - Use the established color scheme (green-themed for sustainability)
   - Keep responsive design in mind for mobile compatibility
   - Use custom CSS sparingly and organized in dedicated files

4. **Testing**:
   - Test all features with different user roles
   - Verify database operations create expected records
   - Test edge cases and error handling
   - Ensure cross-browser compatibility

## Code Attribution

==============================Code Attribution==================================
 
Author: w3schools
Link: https://www.w3schools.com/html/
Date Accessed: 08 May 2025
 
Author: w3schools
Link: https://www.w3schools.com/css/
Date Accessed: 08 May 2025
 
Microsfot Identity
Author: Andy Malone MVP
Link: https://www.youtube.com/watch?v=zS79FDhAhBs
Date Accessed: 08 May 2025
 
Database Work
Author: Microsoft
Link: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-8.0&tabs=visual-studio
Date Accessed: 08 May 2025
 
LINQ Resource
Author: Grant Riordan
Link: https://www.freecodecamp.org/news/how-to-use-linq/
Date Accessed: 08 May 2025
 
Try Catch
Author: w3schools
Link: https://www.w3schools.com/cs/cs_exceptions.php
Date Accessed: 08 May 2025
 
Upload Image in MVC 
Author: Aurigma
Link: https://www.aurigma.com/upload-suite/developers/aspnet-mvc/how-to-upload-files-in-aspnet-mvc
Date Accessed: 08 May 2025
 
JavaScript
Author: w3schools
Link: https://www.w3schools.com/js/DEFAULT.asp
Date Accessed: 08 May 2025
 
JQuery
Author: w3schools
Link: https://www.w3schools.com/jquery/default.asp
Date Accessed: 08 May 2025
 
Regex Phone Number Validation
Author: Tyler Woodward
Link: https://trestleiq.com/phone-validation-regex-the-what-how-and-pros-and-cons/
Date Accessed: 08 May 2025

---

Thank you for using Agri-Energy Connect! We hope this platform contributes to more sustainable farming practices and broader adoption of renewable energy solutions in agriculture.
