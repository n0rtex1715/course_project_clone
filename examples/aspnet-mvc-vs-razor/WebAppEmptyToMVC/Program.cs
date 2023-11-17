using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebAppEmptyToMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WebAppDBContext>(options => options.UseSqlite(GetConnString()));
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie();
builder.Services.AddAuthorization();

// Build and configure app
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Map routes
app.MapControllerRoute(
	name: default,
	pattern: "{controller=Main}/{action=Index}");

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