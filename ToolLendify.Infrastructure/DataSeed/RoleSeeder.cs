using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Infrastructure.DataSeed
{
	public class RoleSeeder
	{
		public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			List<string> roles = new List<string> { "Administrator", "Client", "Owner" };
			foreach(var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}
	}
}
