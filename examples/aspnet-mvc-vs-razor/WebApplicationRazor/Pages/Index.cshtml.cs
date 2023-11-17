using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using WebApplicationRazor.Services;

namespace WebApplicationRazor.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
        private readonly StringGeneratorService _stringGenerator;

        public List<string> Strings { get; set; } = new List<string>();

        public IndexModel(
			ILogger<IndexModel> logger,
            StringGeneratorService stringGenerator)
		{
			_logger = logger;
			_stringGenerator = stringGenerator;
		}

		public void OnGet()
        {
            _logger.LogInformation("{PageModel}.{method} was called at {time}", nameof(IndexModel), nameof(OnGet), DateTime.Now.ToString("u"));
            Strings = _stringGenerator.Generate().ToList();
        }
	}
}