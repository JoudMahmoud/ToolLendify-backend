using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Application.DTOs
{
	public class ToolDto
	{
		[Required]
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		public int Model { get; set; }
		public int PricePerDay { get; set; }
		public bool IsAvailable { get; set; }
		public AddressDto Address { get; set; }
		public string OwnerID { get; set; }
		public string? CategoryName { get; set; }

		

	}
}
