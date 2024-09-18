using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Application.DTOs
{
	public class LogginUserDto
	{
		[Required]
		public string email {  get; set; }
		[Required]
		public string Password { get; set; }
		[DefaultValue(false)]
		public  bool RememberMe { get; set; }
	}
}
