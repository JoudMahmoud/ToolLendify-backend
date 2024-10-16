using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolLendify.Application.DTOs;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public UserProfileController(UserManager<User> userManager,IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserProfileInfo()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Invalid user token");
            }

            var existUser = await _userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (existUser == null) {
                return NotFound("User not found");
            }

            var userDto =  _mapper.Map<UserDto>(existUser);
            if (existUser.Address != null) {
                userDto.Address = _mapper.Map<AddressDto>(existUser.Address);
            }
            return Ok(userDto);
        }

        [HttpPut("update-username")]
        public async Task<IActionResult> UpdateUsername(string id,[FromBody] string username)
        {
            if (string.IsNullOrEmpty(username))
            { return BadRequest(username); }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            { return NotFound("user not found"); }
            user.UserName = username;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) 
            { 
                return Ok(new { message = "Username updated successfully", newUsername = username });
            }
            return BadRequest(result.Errors);
        }

      

		[HttpPut("update-useraddress")]
		public async Task<ActionResult> UpdateAddress(string userId, AddressDto updateAddress)
		{
			if (!ModelState.IsValid)
			{ return BadRequest(ModelState); }
			var existUser = await _userManager.FindByIdAsync(userId);
			if (existUser == null)
			{ return NotFound("User not found"); }
			var address = _mapper.Map<Address>(updateAddress);

			existUser.Address = address;
			var result = await _userManager.UpdateAsync(existUser);
			if (result.Succeeded)
			{
				return Ok("Address added successfully");
			}
			return BadRequest("Failed to  update user address");
		}

        [HttpPut("update-userImage")]
        public async Task<IActionResult> updateUserImage(string userId, string imageUrl)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existUser = await _userManager.FindByIdAsync(userId);
            if (existUser == null)
            { return NotFound("user not found"); }
            existUser.ImageUrl = imageUrl;
            var result = await _userManager.UpdateAsync(existUser);
            if (result.Succeeded)
            {
                return Ok(new { message = "user image updated successed", imageUrl = existUser.ImageUrl });
            }
            return BadRequest(result.Errors);
        }
        [HttpPut("update-useremail")]
        public async Task<IActionResult> UpdateUserEmail(string userId, string email)
        {
            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }
            var existUser = await _userManager.FindByIdAsync(userId);
            if (existUser == null)
            { return NotFound("User not found"); }

            existUser.Email = email;
            var result = await _userManager.UpdateAsync(existUser);
            if (result.Succeeded)
            { return Ok(new { message = "User email updated successfully", email = existUser.Email }); }

            return BadRequest(result.Errors);
        }

        [HttpPut("update-userpassword")]
        public async Task<IActionResult> UpdateUserPassword(string userId, UpdatePasswordDto updatePasswordDto)
        {
            if (!ModelState.IsValid) 
            { return BadRequest(ModelState); }

            var existUser = await _userManager.FindByIdAsync(userId);
            if (existUser == null)
            { return NotFound("User not found");}
            var isPasswordCorrect = await _userManager.CheckPasswordAsync(existUser, updatePasswordDto.CurrentPassword);
            if (!isPasswordCorrect)
            { return BadRequest("Current password is incorrect"); }

            var passwordChangeResult = await _userManager.ChangePasswordAsync(existUser, updatePasswordDto.CurrentPassword, updatePasswordDto.NewPassword);
            if (passwordChangeResult.Succeeded)
            {
                return Ok(new { message = "User password updated successfully", password = updatePasswordDto.NewPassword });
            }
            return BadRequest(passwordChangeResult.Errors);
        }
	}
}
