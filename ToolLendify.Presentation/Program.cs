using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToolLendify.Application.Automapper;
using ToolLendify.Application.Services;
using ToolLendify.Application.Services.Validators;
using ToolLendify.Domain.Entities;
using ToolLendify.Domain.Interfaces;
using ToolLendify.Infrastructure.DataSeed;
using ToolLendify.Infrastructure.DbContext;
using ToolLendify.Infrastructure.Repositories;

namespace ToolLendify.Presentation
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			
			#region Configure Services

			// Add services to the container
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			//register AutoMapper
			builder.Services.AddAutoMapper(typeof(MappingProfile));

			// Configure DbContext
			builder.Services.AddDbContext<ToolLendifyDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
				sqlOptions => sqlOptions.EnableRetryOnFailure());
			});



			// Register Identity services
			builder.Services.AddIdentity<User, IdentityRole>(options =>
			{
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";

				// Require confirmed email account for sign-in
				options.SignIn.RequireConfirmedAccount = false;

				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequiredLength = 8;
				options.Password.RequiredUniqueChars = 1;
			})
			.AddUserValidator<CustomUserValidator<User>>()
			.AddEntityFrameworkStores<ToolLendifyDbContext>()
			.AddDefaultTokenProviders();


			// Configure Authentication
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
					ValidAudience = builder.Configuration["JWT:ValidAudience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
				};
			});


			// Register custom services
			builder.Services.AddScoped<RoleService>();
			builder.Services.AddScoped<IToolRepository, ToolRepository>();
			builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
			builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();

			//builder.Services.AddScoped<DataSeeder>();


			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowSpecificOrigin", builder =>
				{
					builder.WithOrigins("http://localhost:4200") // Frontend URL
						   .AllowAnyMethod()
						   .AllowAnyHeader()
						   .AllowCredentials();
				});
			});

			#endregion
			
			var app = builder.Build();

			#region Seed Data

			// Seed roles and data
			#region Seed Data

			// Seed roles and data
			using (var scope = app.Services.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>(); // Define loggerFactory here
				var logger = loggerFactory.CreateLogger<Program>(); // Create logger instance

				try
				{
					await RoleSeeder.SeedRolesAsync(roleManager);
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "Error occurred while seeding roles."); // Use the logger here
				}

				// Get required services for database migration and data seeding
				var _dbContext = scope.ServiceProvider.GetRequiredService<ToolLendifyDbContext>();

				try
				{
					// Migrate the database
					await _dbContext.Database.MigrateAsync(); // Update database

					// Seed additional data (tools, categories, owners, etc.)
					await ToolLendifyContextSeed.seedAsync(_dbContext); // Data Seeding
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "An error has occurred during apply the migration."); // Log using the same logger
				}
			}

			#endregion


			#endregion

			#region Configure Middleware

			// Configure the HTTP request pipeline
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("AllowSpecificOrigin");
			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();

			// Map controllers
			app.MapControllers();

			#endregion

			app.Run();
		}
	}
}
