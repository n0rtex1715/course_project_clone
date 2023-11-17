using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebAppEmptyToRazorPages.Extensions;
using WebAppEmptyToRazorPages.Models;

namespace WebAppEmptyToRazorPages.Pages.Account
{
    public class LoginPageModel : PageModel
    {
        private readonly ILogger<LoginPageModel> _logger;
        private readonly WebAppDBContext _db;

        [BindProperty]
        [Required(ErrorMessage = "Incorrect login given")]
        public string LoginOrEmail { get; set; }

		[BindProperty]
		[Required(ErrorMessage = "Incorrect password given")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public LoginPageModel(ILogger<LoginPageModel> logger, WebAppDBContext context)
        {
            _logger = logger;
            _db = context;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            var userToLogin = _db.Users.Where(u => u.Login == LoginOrEmail || u.Email == LoginOrEmail).SingleOrDefault();
            if (userToLogin is null)
            {
                ModelState.AddModelError("isLoginFailed", "Bad login or email");
                return Page();
            }
            if (Password is null || userToLogin.Password != Password.ToHash())
            {
                ModelState.AddModelError("isLoginFailed", "Bad password");
                return Page();
            }

            Authenticate(LoginOrEmail);
            return RedirectToPage("../Index");
        }
        private void Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id)).Wait();
        }

    }
}
