using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Sofarashel.Application.Services.Interfaces;
using Sofarashel.Domain.Enums.Account;
using Sofarashel.ViewModels.Account;
using System.Security.Claims;

namespace Sofarashel.Web.Controllers
{
    public class AccountController(IAccountServices accountServices):Controller
    {
        private readonly IAccountServices _accountServices = accountServices;
        #region Login
        [Route("Login")]
     

        public async Task<IActionResult> Login(string ReturnUrl = "/")
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost("Login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login, string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            if (!ModelState.IsValid)
            {
                return View(login);
            }



            var result = await _accountServices.LoginUserAsync(login);


            if (result == LoginUserResult.NotFound)
            {
                ModelState.AddModelError("", "اطلاعات وارد شده صحیح نمی باشد");
                return View(login);
            }

          

            var user = await _accountServices.GetUserByUserNameAsync(login.UserName);
            var claims = new List<Claim>() {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim("FullName",$"{user.FirstName} {user.LastName}"),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };
            await HttpContext.SignInAsync(principal, properties);

            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            return Redirect("/");

        }
        #endregion
    }
}
