﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Domain.Interfaces
{
	public interface IToolRepository
	{
		Task<IEnumerable<Tool>> getAllTools();
		Task<Tool> addTool(Tool tool);
		Task<Tool?> GetToolById(int id);
		Task<IEnumerable<Tool>> GetToolByName(string name);
		void DeleteTool(Tool toolToRemove);
		Task<bool> updateTool(Tool updatedTool);
		Task<IEnumerable<Tool>> GetOwnerTools(string ownerId);
		Task<IEnumerable<Tool>> GetAvailableTools();
		void Save();
	}
}
