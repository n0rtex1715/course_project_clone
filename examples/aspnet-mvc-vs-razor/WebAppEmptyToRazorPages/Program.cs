using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebAppEmptyToRazorPages.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WebAppDBContext>(options => options.UseSqlite(GetConnString()));
builder.Services.AddRazorPages();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie();
builder.Services.AddAuthorization();

// Build and configure app
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Map routes
app.MapRazorPages();

app.Run();


// Gen connString for sqlite database
static string GetConnString()
{
	var folder = Environment.SpecialFolder.Personal;
	var path = Environment.GetFolderPath(folder);
	var dbPath = Path.Join(path, "WebApp.db");
	var connectionString = $"Data source={dbPath}";
	return connectionString;
}