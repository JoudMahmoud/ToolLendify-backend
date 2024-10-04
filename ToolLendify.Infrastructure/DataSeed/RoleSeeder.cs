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
				 try
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            Console.WriteLine($"Error seeding role '{role}': {ex.Message}");
            // You can throw or continue based on your needs
        }
			}
		}
	}
}
