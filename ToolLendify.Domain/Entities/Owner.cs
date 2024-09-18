using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Domain.Entities
{
	public class Owner:User
	{
		public ICollection<Tool> OwnedTools { get; set; }
	}
}
