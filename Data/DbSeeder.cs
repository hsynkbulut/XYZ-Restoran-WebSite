using Microsoft.AspNetCore.Identity;
using OnlineRestaurantProject.Models;

namespace OnlineRestaurantProject.Data
{
	public class DbSeeder
	{
		public static async Task SeedDefaultData(IServiceProvider serviceProvider)
		{
			var userMgr = serviceProvider.GetService<UserManager<IdentityUser>>();
			var roleMgr = serviceProvider.GetService<RoleManager<IdentityRole>>();

			await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
			await roleMgr.CreateAsync(new IdentityRole(Roles.Kullanici.ToString()));

			var admin = new IdentityUser
			{
				UserName = "admin@gmail.com",
				Email = "admin@gmail.com",
				EmailConfirmed = true,

			};
			var isUserExists = await userMgr.FindByEmailAsync(admin.Email);

			if (isUserExists is null)
			{
				await userMgr.CreateAsync(admin, "Admin1_");
				await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
			}

		}
	}
}
