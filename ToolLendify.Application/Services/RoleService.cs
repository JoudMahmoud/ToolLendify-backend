using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLendify.Domain.Interfaces;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Application.Services
{
	public class RoleService
	{
		private readonly UserManager<User> _userManager;
		public RoleService(UserManager<User> userManager)
		{ _userManager = userManager; }

		public async Task AddUserToRoleAsync(User user, string roleName)
		{
			if(!await _userManager.IsInRoleAsync(user, roleName))
			{
				await _userManager.AddToRoleAsync(user, roleName);
			}
		}
		public async Task RemoveUserFromRoleAsync(User user, string roleName)
		{
			if(await _userManager.IsInRoleAsync(user, roleName))
			{
				await _userManager.RemoveFromRoleAsync(user, roleName);
			}
		}
	}
}