using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppEmptyToRazorPages.Extensions;
using WebAppEmptyToRazorPages.Models;

namespace WebAppEmptyToRazorPages.Pages.Account
{
    public class RegisterPageModel : PageModel
    {
        private readonly ILogger<RegisterPageModel> _logger;
        private readonly WebAppDBContext _db;

        [BindProperty]
        [Required(ErrorMessage = "Incorrect login given")]
        public string Login { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Incorrect email given")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Incorrect password given")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Passwords are not the same")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        public RegisterPageModel(ILogger<RegisterPageModel> logger, WebAppDBContext context)
        {
            _logger = logger;
            _db = context;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || Password != ConfirmPassword)
            {
                ModelState.AddModelError("isRegFailed", "Passwords incorrect");
                return Page();
            }
            if (_db.Users.Where(u => u.Login == Login || u.Login == Email || u.Email == Login || u.Email == Email).Any())
            {
                ModelState.AddModelError("isRegFailed", "Login or Email already taken");
                return Page();
            }

            var user = new User();
            user.Login = Login;
            user.Email = Email;
            user.Password = Password.ToHash();

            _db.Users.Add(user);
            _db.SaveChanges();

            return RedirectToPage("./Login");
        }
    }
}
