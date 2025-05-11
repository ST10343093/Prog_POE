using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Prog_POE.Controllers
{
    // Base controller that provides common functionality for authentication and authorization
    // All controllers except Landing and Auth should inherit from this
    public class BaseController : Controller
    {
        // Method that runs before each action to verify authentication and prepare common ViewBag properties
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // Allow anonymous access to landing page and authentication-related pages
            // These pages should be accessible without requiring user login
            if (context.ActionDescriptor.DisplayName.Contains("Landing") ||
                context.Controller is AuthController)
            {
                return;
            }

            // Redirect unauthenticated users to login page
            // This creates a secure application where protected resources require authentication
            if (HttpContext.Session.GetString("UserId") == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            // Make user information available to all views via ViewBag
            // This allows for consistent user interface personalization across the application
            ViewBag.UserId = HttpContext.Session.GetString("UserId");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
        }
    }
}