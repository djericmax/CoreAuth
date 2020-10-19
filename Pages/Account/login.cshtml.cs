using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoreAuth.Pages.Account
{
    public class loginModel : PageModel
    {
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(LoginInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!IsUserAuthenticated(inputModel.UserName, inputModel.Password))
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, inputModel.UserName)
            };

            var userIdentity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);

            return Redirect("/");
        }

        public async Task<IActionResult> OnPostLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Account/Login");
        }

        private bool IsUserAuthenticated(string userName, string password)
        {
            //simulação de autenticação no banco de daos.
            return true;
        }
    }
}