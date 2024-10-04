using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToolLendify.Application.DTOs;
using ToolLendify.Domain.Entities;
using ToolLendify.Domain.Interfaces;

namespace ToolLendify.Presentation.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class toolController : ControllerBase
	{
		#region fields
		private readonly IMapper _mapper;
		private readonly IToolRepository _toolRepo;
		private readonly UserManager<User> _userManager;
		#endregion
		public toolController(IMapper mapper, IToolRepository toolRepo, UserManager<User> userManager)
		{
			_mapper = mapper;
			_toolRepo = toolRepo;
			_userManager = userManager;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<ToolDto>>> GetAllTools()
		{
			var toolsList = await _toolRepo.getAllTools();
			if (toolsList.Count() == 0)
			{
				return NotFound();
			}
			var toolsDtoList = _mapper.Map<List<ToolDto>>(toolsList);
			return Ok(toolsDtoList);
		}
		
		[HttpPost]
		public async Task<ActionResult<ToolDto>> addTool([FromBody] ToolDto addToolDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Tool tool = _mapper.Map<Tool>(addToolDto);

			var user = await _userManager.FindByIdAsync(addToolDto.OwnerID);
			if (user == null)
			{
				return NotFound("user not found");
			}
			if(!await _userManager.IsInRoleAsync(user, "Owner"))
			{
				var result = await _userManager.AddToRoleAsync(user, "Owner");
				if (!result.Succeeded)
				{
					return BadRequest("Failed to assign owner role");
				}
			}


			var toolAdded = await _toolRepo.addTool(tool);

			if (toolAdded == null)
			{ return BadRequest(addToolDto); }

			_toolRepo.Save();

			return Ok(addToolDto);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<ToolDto>> GetToolById(int id)
		{
			var tool = await _toolRepo.GetToolById(id);
			if (tool == null)
			{
				return NotFound();
			}
			var ToolDto = _mapper.Map<Tool>(tool);

			return Ok(ToolDto);
		}
		[HttpDelete("{id}")]
		
		public async Task<ActionResult> DeleteTool(int id)
		{
			var existingTool = await _toolRepo.GetToolById(id);
			if (existingTool == null)
			{
				return NotFound();
			}
			_toolRepo.DeleteTool(existingTool);
			_toolRepo.Save();
			return Ok("Tool deleted");
		}
		[HttpPatch]
		public async Task<ActionResult<ToolDto>> UpdateTool(int id, [FromBody] ToolDto toolToEdit)
		{
			var existingTool = await _toolRepo.GetToolById(id);
			if (existingTool == null) { return NotFound(); }

			_mapper.Map(toolToEdit, existingTool);

			bool updated = await _toolRepo.updateTool(existingTool);
			if (updated)
			{
				_toolRepo.Save();
				//_mapper.Map(source, destination);
				_mapper.Map(existingTool, toolToEdit);
				return Ok(toolToEdit);
			}

			return StatusCode(500, "Unable to update the tool.");
		}

		[HttpGet("ownerTools")]
		public async Task<ActionResult<IEnumerable<ToolDto>>> GetOwnerTools([FromQuery]string id)
		{
			var ownerTools = await _toolRepo.GetOwnerTools(id);
			if(ownerTools.Count()==0) 
			{ return NotFound(); }
			_mapper.Map<List<ToolDto>>(ownerTools);
			return Ok(ownerTools);
		}
		[HttpGet("availableTools")]
		 public async Task<ActionResult<IEnumerable<ToolDto>>> GetAvailableTools()
		{
			var availableTools = await _toolRepo.GetAvailableTools();
			if(availableTools.Count()==0)
			{
				return NotFound();
			}
			_mapper.Map<List<ToolDto>>(availableTools);
			return Ok(availableTools);
		}
		[HttpGet("get-by-name/{name}")]
		public async Task<ActionResult<IEnumerable<ToolDto>>> GetToolByName(string name)
		{
			var tool = await _toolRepo.GetToolByName(name);
			if(tool.Count() == 0)
			{
				return NotFound();
			}
			var ToolDto = _mapper.Map<List<ToolDto>>(tool);
			return Ok(ToolDto);
		}
	}
}
