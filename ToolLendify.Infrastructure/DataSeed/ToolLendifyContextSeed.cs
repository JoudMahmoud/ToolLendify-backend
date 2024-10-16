using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToolLendify.Domain.Entities;
using ToolLendify.Infrastructure.DbContext;

namespace ToolLendify.Infrastructure.DataSeed
{
	public class ToolLendifyContextSeed
	{
		public static async Task seedAsync(ToolLendifyDbContext _dbContext)
		{
			if (_dbContext.Users.Count()==0)
			{
				var usersData = File.ReadAllText("../ToolLendify.Infrastructure/DataSeed/SeedingFiles/User.json");
				var users = JsonSerializer.Deserialize<List<User>>(usersData);
				if (users?.Count()>0) //(users!=null && users.Count() > 0)
				{
					foreach (var user in users)
					{
						_dbContext.Set<User>().Add(user);
					}
					await _dbContext.SaveChangesAsync();
				}
			}
			if (_dbContext.Categories.Count()==0)
			{
				var categoriesData = File.ReadAllText("../ToolLendify.Infrastructure/DataSeed/SeedingFiles/Category.json");
				var categories = JsonSerializer.Deserialize
					<List<Category>>(categoriesData);
				if (categories?.Count()>0)
				{
					foreach (var category in categories)
					{
						_dbContext.Set<Category>().Add(category);
					}
					await _dbContext.SaveChangesAsync();
				}
			}

			if (_dbContext.Tools.Count()==0)
			{
				var toolsData = File.ReadAllText("../ToolLendify.Infrastructure/DataSeed/SeedingFiles/Tool.json");
				var tools = JsonSerializer.Deserialize<List<Tool>>(toolsData);
				if (tools?.Count()>0)
				{
					foreach (var tool in tools)
					{
						_dbContext.Set<Tool>().Add(tool);
					}
					await _dbContext.SaveChangesAsync();
				}
			}

			if (_dbContext.Reviews.Count()==0)
			{
				var reviewsData = File.ReadAllText("../ToolLendify.Infrastructure/DataSeed/SeedingFiles/Review.json");
				var reviews = JsonSerializer.Deserialize<List<Review>>(reviewsData);
				if (reviews?.Count()>0)
				{
					foreach (var review in reviews)
					{
						_dbContext.Set<Review>().Add(review);
					}
					await _dbContext.SaveChangesAsync();
				}
			}

		}
	}
}
