using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Domain.Interfaces
{
	public interface IOwnerRepository
	{
		Task<IEnumerable<Owner>> GetAllOwners();
	}
}
