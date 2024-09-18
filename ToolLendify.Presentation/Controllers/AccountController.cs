using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToolLendify.Application.DTOs;
using ToolLendify.Application.Services;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Presentation.Controllers
{
	public class AccountController:ControllerBase
	{
		#region Member Data
		private readonly UserManager<User> _userManager;
		private readonly RoleService _roleService;
		private readonly IConfiguration _configuration;
		#endregion

		#region Constructor
		public AccountController(UserManager<User> userManager, RoleService roleService, IConfiguration configuration)
		{
			_userManager = userManager;
			_roleService = roleService;
			_configuration = configuration;
		}
		#endregion

		#region Registration 
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody]RegisterUserDto registerUser)
		{
			var existUser = await _userManager.FindByEmailAsync(registerUser.Email);
			if (existUser != null)
			{
				return BadRequest(new { error = "Email already exists" });
			}
			if(ModelState.IsValid)
			{
				var user = new User
				{
					UserName = registerUser.UserName,
					Email = registerUser.Email
				};

				IdentityResult result = await _userManager.CreateAsync(user, registerUser.Password);
				if (result.Succeeded)
				{
					await _roleService.AddUserToRoleAsync(user, "Client");
					return Ok();
				}
				else
				{
					return BadRequest(result.Errors);
				}
			}
			return BadRequest(ModelState);
		}
		#endregion

		#region Loggin
		[HttpPost("login")]
		public async Task<IActionResult> Loggin([FromBody]LogginUserDto logginUser)
		{
			if (ModelState.IsValid)
			{
				//cheak by name 
				User? user = await _userManager.FindByEmailAsync(logginUser.email);
				if (user != null)
				{
					bool found = await _userManager.CheckPasswordAsync(user, logginUser.Password);
					if (found)
					{
						//get role
						var roles = await _userManager.GetRolesAsync(user);
						var token = GenerateJwtToken(user, roles, out DateTime expiration);
						return Ok(new
						{
							token = token,
							expiration = DateTime.UtcNow.AddHours(1)
						});
					}
					return Unauthorized();
				}
				return Unauthorized();
			}
			return Unauthorized();
		}


		#endregion
		#region Generate JWT Token
		private string GenerateJwtToken(User user, IList<string> roles, out DateTime expiration)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.UserName),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			foreach (var itemRole in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, itemRole));
			}

			SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
			SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
			expiration = DateTime.UtcNow.AddHours(1);
			//create Token 
			JwtSecurityToken myToken = new JwtSecurityToken (
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				claims: claims,
				expires: expiration,
				signingCredentials: signingCred
				);
			return new JwtSecurityTokenHandler().WriteToken(myToken);
		}
		#endregion
	}
}
