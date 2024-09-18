using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Domain.Entities
{
	public enum RentalStatus
	{
		Request,
		Accept
	}
	public class Rental:Base
	{
		public RentalStatus Status { get; set; }
		public DateOnly StartDate { get; set; }
		public DateOnly EndDate { get; set; }
		public ICollection<Tool> tools { get; set; }
		public Payment Payment { get; set; }
		[ForeignKey("User")]
		public string UserId { get; set; }
		public User User { get; set; }
	}
}
