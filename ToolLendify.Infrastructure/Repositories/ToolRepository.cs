using Microsoft.AspNetCore.Mvc;
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
	public class ToolRepository: IToolRepository
	{
		public ToolLendifyDbContext _dbcontext;

		public ToolRepository(ToolLendifyDbContext dbcontext)
		{ _dbcontext = dbcontext; }

		public async Task<IEnumerable<Tool>> getAllTools()
		{
			return await _dbcontext.Tools.ToListAsync();
		}

		public async Task<Tool> addTool(Tool newTool)
		{
			_dbcontext.Tools.Add(newTool);
			return newTool;
		}
		public async Task<Tool?> GetToolById(int id)
		{
			
			return await _dbcontext.Tools.FirstOrDefaultAsync(t=>t.Id == id);
		}
		public async Task<bool> updateTool(Tool updatedTool)
		{
			_dbcontext.Entry(updatedTool).State = EntityState.Modified;
			return true;
		}
		public async void DeleteTool(Tool toolToRemove)
		{
			_dbcontext.Tools.Remove(toolToRemove);
		}

		public async Task<IEnumerable<Tool>> GetOwnerTools(string ownerId)
		{
			return await _dbcontext.Tools.Where(t=>t.OwnerID == ownerId).ToListAsync();
		}
		public void Save()
		{
			_dbcontext.SaveChanges();
		}

	}
}
