using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppEmptyToMVC.Models;

namespace WebAppEmptyToMVC.Controllers
{
	[Authorize]
	public class MainController : Controller
	{
		private readonly ILogger<MainController> _logger;
		private readonly WebAppDBContext _db;
		public MainController(ILogger<MainController> logger, WebAppDBContext context) 
		{
			_logger = logger;
			_db = context;
		}

		public async Task<IActionResult> Index()
		{
			return View(await _db.Users.ToListAsync());
		}
	}
}
