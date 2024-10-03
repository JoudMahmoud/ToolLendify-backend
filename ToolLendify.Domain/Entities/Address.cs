using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Domain.Entities
{
	public class Address:Base
	{
		public string StreetAddress { get; set; }
		public string District {  get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public float? Latitude { get; set; }
		public float? Longitude { get; set; }
		public string Phone { get; set; }

		public override string ToString()
		{
			// Format the address as "StreetAddress, City, PostalCode, Country"
			return $"{StreetAddress}, {City}, {PostalCode}, {Country}";
		}

		public bool IsValidForGeocoding()
		{
			return !string.IsNullOrEmpty(StreetAddress) &&
				   !string.IsNullOrEmpty(City) &&
				   !string.IsNullOrEmpty(Country);
		}

		// Method to check if the address already has latitude and longitude
		public bool HasCoordinates()
		{
			return Latitude.HasValue && Longitude.HasValue;
		}
	}
}
