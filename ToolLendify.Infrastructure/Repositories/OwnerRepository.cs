using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLendify.Application.DTOs;
using ToolLendify.Domain.Entities;
using ToolLendify.Domain.Interfaces;
using ToolLendify.Infrastructure.DbContext;

namespace ToolLendify.Infrastructure.Repositories
{
	public class OwnerRepository:IOwnerRepository
	{
		private ToolLendifyDbContext _dbcontext;
		private readonly UserManager<User> _userManager;
		public OwnerRepository(ToolLendifyDbContext dbContext, UserManager<User> userManager) {
			_dbcontext = dbContext;
			_userManager = userManager;
		}

		public async Task<IEnumerable<Owner>> GetAllOwners()
		{
			var ownerUsers = await _userManager.GetUsersInRoleAsync("owner");
			return ownerUsers.Select(user =>new Owner
			{
				Id = user.Id,
				UserName = user.UserName,
				ImageUrl = user.ImageUrl,
				Address = user.Address,
				OwnedTools= new List<Tool>()
			}).ToList();
		}
		
	}
}
