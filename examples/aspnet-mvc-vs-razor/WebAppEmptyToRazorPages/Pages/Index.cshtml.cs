using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppEmptyToRazorPages.Models;

namespace WebAppEmptyToRazorPages.Pages
{
	[Authorize]
	public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly WebAppDBContext _db;

        public IndexModel(ILogger<IndexModel> logger, WebAppDBContext context)
        {
            _logger = logger;
            _db = context;
        }

		public void OnGet() {}

        public IEnumerable<User> GetUsers() => _db.Users;
    }
}
