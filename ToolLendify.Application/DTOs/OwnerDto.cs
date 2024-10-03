﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Application.DTOs
{
	public class OwnerDto
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string ImageUrl { get; set; }
		public Address Address { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }


	}
}
