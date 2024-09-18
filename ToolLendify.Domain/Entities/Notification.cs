using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Domain.Entities
{
	public enum NotificationType
	{
		Info,
		Warning, 
		Error
	}
	public class Notification:Base
	{
		public string Message { get; set; }
		public string Type { get; set; }
		public bool IsRead { get; set; }
		public DateTime TimesTamp {  get; set; }

		[ForeignKey("User")]
		public string UserId { get; set; }
		public User User { get; set; }

	}
}
