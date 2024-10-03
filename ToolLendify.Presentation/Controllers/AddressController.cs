using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToolLendify.Application.DTOs;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AddressController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;

		AddressController(UserManager<User> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}


		[HttpPost]
		public async Task<ActionResult> CreateAddress(AddressDto updateAddress, string userId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var existUser = await _userManager.FindByIdAsync(userId);
			if (existUser == null)
			{
				return NotFound("User not found");
			}
			var address = _mapper.Map<Address>(updateAddress);

			existUser.Address = address;
			var result = await _userManager.UpdateAsync(existUser);
			if (result.Succeeded)
			{
				return Ok("Address added successfully");
			}
			return BadRequest("Failed to  update user address");
		}

	}
}
