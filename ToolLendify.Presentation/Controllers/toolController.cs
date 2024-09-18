using AutoMapper;
using Microsoft.AspNetCore.Http;
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
		#endregion
		public toolController(IMapper mapper, IToolRepository toolRepo)
		{
			_mapper = mapper;
			_toolRepo = toolRepo;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<toolDto>>> GetAllTools()
		{
			var toolsList = await _toolRepo.getAllTools();
			if (toolsList.Count() == 0)
			{
				return NotFound();
			}
			var toolsDtoList = _mapper.Map<List<toolDto>>(toolsList);
			return Ok(toolsDtoList);
		}
		[HttpPost]
		public async Task<ActionResult<toolDto>> addTool([FromBody] toolDto addToolDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Tool tool = _mapper.Map<Tool>(addToolDto);

			var toolAdded = await _toolRepo.addTool(tool);

			if (toolAdded == null)
			{ return BadRequest(addToolDto); }

			_toolRepo.Save();

			return Ok(addToolDto);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Tool>> GetToolById(int id)
		{
			var tool = await _toolRepo.GetToolById(id);
			if (tool == null)
			{
				return NotFound();
			}
			return Ok(tool);
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
		public async Task<ActionResult<toolDto>> UpdateTool([FromRoute]int id, [FromBody] toolDto toolToEdit)
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
		public async Task<ActionResult<IEnumerable<toolDto>>> GetOwnerTools([FromQuery]string id)
		{
			var ownerTools = await _toolRepo.GetOwnerTools(id);
			if(ownerTools.Count()==0) 
			{ return NotFound(); }
			_mapper.Map<List<toolDto>>(ownerTools);
			return Ok(ownerTools);
		}
	}
}
