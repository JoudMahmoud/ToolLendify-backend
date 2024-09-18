using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Domain.Entities
{
	public class Review:Base
	{
		public string Comment {  get; set; }
		public int Rating { get; set; }
		[ForeignKey("User")]
		public string UserId { get; set; }
		public User User { get; set; }
		[ForeignKey("Tool")]
		public int ToolId { get; set; }
		public Tool	Tool { get; set; }
	}
}
