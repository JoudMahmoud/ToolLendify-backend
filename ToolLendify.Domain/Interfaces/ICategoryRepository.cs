using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Domain.Interfaces
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<Category>> getAllCategories();
		/*Task<IEnumerable<categorydto>> getAllCategories();*/
	}
}
