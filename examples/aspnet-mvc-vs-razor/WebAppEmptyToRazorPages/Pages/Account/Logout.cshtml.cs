using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Authorization;
using WebAppEmptyToRazorPages.Models;

namespace WebAppEmptyToRazorPages.Pages.Account
{
    //[Authorize]
    public class LogoutModel : PageModel
	{
		private readonly ILogger<LoginPageModel> _logger;
		private readonly WebAppDBContext _db;
		public LogoutModel(ILogger<LoginPageModel> logger, WebAppDBContext context)
		{
			_logger = logger;
			_db = context;

		}
		public IActionResult OnGet()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
			return RedirectToPage("./Login");
		}
    }
}
