using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Domain.Entities
{
	public class User:IdentityUser
	{
		public string? ImageUrl { get; set; }
		public Address? Address { get; set; }
		public ICollection<Notification> Notifications { get; set; }
		public ICollection<Review> Reviews { get; set; }
		public ICollection<Payment> Payments { get; set; }	
	}
}
