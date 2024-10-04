using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToolLendify.Application.DTOs;
using ToolLendify.Domain.Interfaces;

namespace ToolLendify.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		public readonly ICategoryRepository _catRepo;
		public readonly IMapper _mapper;
		public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
		{
			_catRepo = categoryRepository;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAllCategories() 
		{
			var categories = await _catRepo.getAllCategories();
			if (categories.Count() == 0)
			{
				return NotFound();
			}var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
			return Ok(categoriesDto);
		}
		
	}
}
