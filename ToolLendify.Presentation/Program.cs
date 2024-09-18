using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToolLendify.Application.Automapper;
using ToolLendify.Application.Services;
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
			builder.Services.AddAutoMapper(typeof(Program).Assembly);

			//confirure the http request pipeline
			/*if (app.Environment.Isdevelopment())
			{
				app.UseDeveloperExceptionPage();
			}*/

			// Configure DbContext
			builder.Services.AddDbContext<ToolLendifyDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});



			// Register Identity services
			builder.Services.AddIdentity<User, IdentityRole>(options =>
			{
				// Require confirmed email account for sign-in
				options.SignIn.RequireConfirmedAccount = false;

				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequiredLength = 8;
				options.Password.RequiredUniqueChars = 1;
			})
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
			builder.Services.AddAutoMapper(typeof(MappingProfile));
			builder.Services.AddScoped<IToolRepository, ToolRepository>();

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

			// Seed roles
			using (var scope = app.Services.CreateScope())
			{
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				await RoleSeeder.SeedRolesAsync(roleManager);
			}

			#endregion
			
			#region Configure Middleware

			// Configure the HTTP request pipeline
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("AllowSpecificOrigin");
			//app.UserCors("AllowSpecificOrigin");

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
