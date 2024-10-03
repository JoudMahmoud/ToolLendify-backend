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
	public class CategoryRepository:ICategoryRepository
	{
		public ToolLendifyDbContext _dbcontext;

		public CategoryRepository(ToolLendifyDbContext dbcontext)
		{
			_dbcontext = dbcontext;
		}
		public async Task<IEnumerable<Category>> getAllCategories()
		{
			return await _dbcontext.Categories.ToListAsync();
		}

	}
}
