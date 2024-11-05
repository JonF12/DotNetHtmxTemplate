using Microsoft.AspNetCore.Mvc;

namespace DotNetHtmxTemplate.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public IActionResult GetPage(string pageName)
        {
            if (User?.Identity?.IsAuthenticated == false)
            {
                return RedirectToAction("Index", "Authentication");
            }
            if (Request.Headers.ContainsKey("HX-Request"))
            {
                return PartialView(pageName);
            }
            else
            {
                return View(pageName);
            }
        }
        public IActionResult GetPage(string pageName, object model)
        {
            if (User?.Identity?.IsAuthenticated == false) 
            {
                return RedirectToAction("Index", "Authentication");
            }
            if (Request.Headers.ContainsKey("HX-Request"))
            {
                return PartialView(pageName, model);
            }
            else
            {
                return View(pageName, model);
            }
        }
    }
}
