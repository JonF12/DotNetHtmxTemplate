using Microsoft.AspNetCore.Mvc;

namespace DotNetHtmxTemplate.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IActionResult GetPage(string pageName, object model = null)
        {
            if (User?.Identity?.IsAuthenticated == false)
            {
                Response.Headers.TryAdd("HX-Redirect", "/authentication");
                return Unauthorized();
            }

            if (Request.Headers.ContainsKey("HX-Request"))
            {
                return PartialView(pageName, model);
            }

            return View(pageName, model);
        }
    }
}
