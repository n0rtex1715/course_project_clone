using Microsoft.EntityFrameworkCore;

namespace WebAppEmptyToRazorPages.Models;

public class WebAppDBContext : DbContext
{
	public DbSet<User> Users { get; set; }

	public WebAppDBContext(DbContextOptions options) : base (options)
	{
		Database.EnsureCreated();
	}
}
