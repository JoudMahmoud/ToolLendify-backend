using AutoMapper;
using Microsoft.AspNetCore.Http;
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
		public async Task<ActionResult<IEnumerable<categoryDto>>> GetAllCategories() /*when i try to get categories from swagger don't intern this function what is issue in this */
		{
			var categories = await _catRepo.getAllCategories();
			if (categories.Count() == 0)
			{
				return NotFound();
			}var categoriesDto = _mapper.Map<List<categoryDto>>(categories);
			return Ok(categoriesDto);
		}
		
	}
}
