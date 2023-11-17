using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppEmptyToMVC.Models;
using Microsoft.AspNetCore.Authorization;
using WebAppEmptyToMVC.Extensions;
using WebAppEmptyToMVC.ViewModels;

namespace WebAppEmptyToMVC.Controllers
{
    public class AccountController : Controller
	{
		private readonly ILogger<AccountController> _logger;
		private readonly WebAppDBContext _db;

		public AccountController(ILogger<AccountController> logger, WebAppDBContext context)
		{
			_logger = logger;
			_db = context;
		}

        // GET: Account/Register
        public IActionResult Register()
		{
			return View();
		}

		// POST: Account/Register
		[HttpPost]
		public IActionResult Register(RegisterModel regUser)
		{
			if (!ModelState.IsValid || regUser.Password != regUser.ConfirmPassword)
			{
				ModelState.AddModelError("isRegFailed", "Passwords incorrect");
				return View(regUser);
			}
			if (_db.Users.Where(u => u.Login == regUser.Login || u.Login == regUser.Email || u.Email == regUser.Login || u.Email == regUser.Email).Any())
			{
				ModelState.AddModelError("isRegFailed", "Login or Email already taken");
				return View(regUser);
			}

			var user = new User();
			user.Login = regUser.Login;
			user.Email = regUser.Email;
			user.Password = regUser.Password.ToHash();

			_db.Users.Add(user);
			_db.SaveChangesAsync().Wait();

			return RedirectToAction(nameof(Login));
		}

        // GET: Account/Login
        public IActionResult Login()
		{
			return View();
		}

		// POST: Account/Login
		[HttpPost]
		public async Task<IActionResult> Login(LoginModel loginUser, bool failed = false)
		{

			var userToLogin = await _db.Users.Where(u => u.Login == loginUser.LoginOrEmail || u.Email == loginUser.LoginOrEmail).SingleOrDefaultAsync();
			if (userToLogin is null)
			{
				_logger.LogWarning("At {time} Failed login attempt was made with {login}", DateTime.Now.ToString("u"), loginUser.LoginOrEmail);
				ModelState.AddModelError("isLoginFailed", "Bad login or email");
				return View(loginUser);
			}
			if (userToLogin?.Password != loginUser.Password.ToHash())
			{
				_logger.LogWarning("At {time} Failed login attempt was made with {login}", DateTime.Now.ToString("u"), loginUser.LoginOrEmail);
				ModelState.AddModelError("isLoginFailed", "Bad password");
				return View(loginUser);
			}

            Authenticate(loginUser.LoginOrEmail);
			return RedirectToAction(nameof(Index), "Main");
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

		[Authorize]
		public IActionResult Logout()
		{
			HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
			return RedirectToAction(nameof(Login), "Account");
		}


	}
}
