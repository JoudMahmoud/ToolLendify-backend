using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Infrastructure.DbContext
{
	public class ToolLendifyDbContext:IdentityDbContext<User>
	{
		public ToolLendifyDbContext(DbContextOptions<ToolLendifyDbContext>options):base(options)
		{ }
		public DbSet<Owner> Owners { get; set; }
		public DbSet<Tool> Tools { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Payment> Payments { get; set; }
		public DbSet<Rental> Rentals { get; set; }
		public DbSet<Review> Reviews { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			//to make email unique
			builder.Entity<User>()
				.HasIndex(u => u.Email).IsUnique();

			builder.Entity<Payment>()
				.Property(p => p.AmountPaid)
				.HasColumnType("decimal(18,2)");
		}
	}
}
