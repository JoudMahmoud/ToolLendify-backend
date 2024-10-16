using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Domain.Entities
{
	public class Tool :Base
	{
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		public int Model { get; set; }
		public int PricePerDay { get; set; }
		public bool IsAvailable { get; set; }
		public Address Address { get; set; }
		[ForeignKey("Owner")]
		public string OwnerID { get; set; }
		[Required]
		public Owner Owner { get; set; }

		[ForeignKey("Category")]
		public int? CategoryID { get; set; }
		public Category? Category { get; set; }	

		public ICollection<Review>? Reviews { get; set; }
		public ICollection<Rental>? Rentals { get; set; }
	}
}
