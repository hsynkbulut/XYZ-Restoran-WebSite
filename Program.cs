using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurantProject.Data;
using OnlineRestaurantProject.Services.Abstract;
using OnlineRestaurantProject.Services.Implementation;

namespace OnlineRestaurantProject
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));
			builder.Services.AddScoped<ICategoryService, CategoryService>();
			builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI().AddDefaultTokenProviders();

			builder.Services.AddControllersWithViews();

			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.ExpireTimeSpan = TimeSpan.FromDays(1);
				options.SlidingExpiration = true;

				options.AccessDeniedPath = "/Identity/Account/AccessDenied";
				options.LoginPath = "/Identity/Account/Login";
				options.LogoutPath = "/Identity/Account/Logout";
			});

			var app = builder.Build();



			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();
           // asp - area = "Identity" asp - page = "/Account/Manage/Index"

            using (var scope = app.Services.CreateScope())
			{
				await DbSeeder.SeedDefaultData(scope.ServiceProvider);
			}
			app.Run();
		}
	}
}
