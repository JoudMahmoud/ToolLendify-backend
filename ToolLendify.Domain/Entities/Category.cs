using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Domain.Entities
{
	public class Category:Base
	{
		public string Name { get; set; }
		public List<Tool> Tools { get; set; }
	}
}
