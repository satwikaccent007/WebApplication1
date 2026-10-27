
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication3.Middlewares
{
    public class Auth:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var res = context.HttpContext.Session.GetString("Email");
            if (res == null)
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
            }
        }
    }
}
