using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Prog_POE.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // Allow anonymous access to specific pages
            if (context.ActionDescriptor.DisplayName.Contains("Landing") ||
                context.Controller is AuthController)
            {
                return;
            }

            // Check if user is logged in
            if (HttpContext.Session.GetString("UserId") == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            // Set ViewBag variables that will be used across the application
            ViewBag.UserId = HttpContext.Session.GetString("UserId");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            ViewBag.FullName = HttpContext.Session.GetString("FullName");
        }
    }
}