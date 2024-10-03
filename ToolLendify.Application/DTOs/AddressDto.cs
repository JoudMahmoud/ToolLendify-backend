using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Application.DTOs
{
    public class AddressDto
    {
		public string StreetAddress { get; set; }
		public string District { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public float? Latitude { get; set; }
		public float? Longitude { get; set; }
		public string Phone { get; set; }
	}
}
