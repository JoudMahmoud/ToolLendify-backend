using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Application.Services.Validators
{
	public class CustomUserValidator<TUser> : UserValidator<TUser> where TUser : class
	{
		public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
		{
			var result = await base.ValidateAsync(manager, user);

			var errors = result.Errors.ToList();

			// Assuming you have a User class with a UserName property
			var userName = (user as User)?.UserName;

			// Custom validation: Allow spaces in usernames
			if (!string.IsNullOrEmpty(userName) && userName.Contains(" "))
			{
				// Remove the InvalidUserName error if it exists
				errors.RemoveAll(e => e.Code == "InvalidUserName");
			}

			if (errors.Any())
			{
				return IdentityResult.Failed(errors.ToArray());
			}

			return IdentityResult.Success;
		}
	}
}
