using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolLendify.Domain.Entities
{
	public enum PaymentMethod
	{
		CreditCard,
		Paymob, //PayPal
		BankTransfer
	}
	public class Payment:Base
	{ 
		public PaymentMethod PaymentMethod { get; set; }
		public decimal AmountPaid { get; set; }
		[ForeignKey("User")]
		public string UserId { get; set; }
		public User User { get; set; }
		[ForeignKey("Rental")]
		public int RentalId { get; set; }
		[Required]
		public Rental Rental { get; set; }
	}
}
