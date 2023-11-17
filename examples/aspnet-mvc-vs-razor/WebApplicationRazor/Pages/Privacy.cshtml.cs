using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationRazor.Services;

namespace WebApplicationRazor.Pages
{
	public class PrivacyModel : PageModel
	{
		private readonly ILogger<PrivacyModel> _logger;
        private readonly StringGeneratorService _generatorService;

        public List<string> Strings { get; set; } = new()
		{
		"123", "123", "321", "wololo"
		};

        public PrivacyModel(
			ILogger<PrivacyModel> logger,
			StringGeneratorService generatorService)
		{
			_logger = logger;
			_generatorService = generatorService;
		}

		public void OnGet()
		{
			Strings = _generatorService.Generate().ToList();
        }
	}
}