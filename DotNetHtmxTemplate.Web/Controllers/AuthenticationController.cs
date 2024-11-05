using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DotNetHtmxTemplate.Web.Models;

namespace DotNetHtmxTemplate.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        [Route("/authentication")]
        public IActionResult Index()
        {
            return View("Pages/Authentication.cshtml");
        }

        [HttpPost]
        [Route("/authentication/login")]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel)
        {
            if (loginModel.Username == "test" && loginModel.Password== "123")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginModel.Username),
                    new Claim(ClaimTypes.Role, "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Remember me
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(24)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );

                return Redirect("/");
            }
            return RedirectToAction("Index", "Authentication");
        }
    }
}
