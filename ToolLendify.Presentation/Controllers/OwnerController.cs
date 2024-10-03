﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using ToolLendify.Application.DTOs;
using ToolLendify.Domain.Entities;
using ToolLendify.Domain.Interfaces;
using ToolLendify.Infrastructure.Repositories;

namespace ToolLendify.Presentation.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OwnerController : ControllerBase
	{
		private readonly IOwnerRepository _ownerRepo;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		public OwnerController(IMapper mapper, IOwnerRepository ownerRepo, UserManager<User> usermanager)
		{
			_ownerRepo = ownerRepo;
			_mapper = mapper;
			_userManager = usermanager;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OwnerDto>>> GetAllOwners()
		{
			var ownerList = await _ownerRepo.GetAllOwners();
			if (ownerList.Count() == 0)
			{
				return NotFound("No owners found.");
			}
			var ownerListDto = _mapper.Map<List<OwnerDto>>(ownerList);
			return Ok(ownerListDto);

		}

		[HttpGet("owners-by-name")]
		public async Task<ActionResult<IEnumerable<OwnerDto>>> GetOwnersByName(string name)
		{
			var users = await _userManager.Users
				.Where(user => user.UserName.Contains(name))
				.ToListAsync();

			

			var owners = new List<User>();
			foreach (var user in users)
			{
				if (await _userManager.IsInRoleAsync(user, "Owner"))
				{
					owners.Add(user);
				}
			}
			if (owners.Count() == 0)
			{
				return NotFound("No owners found with the given name.");
			}
			var ownerListDto = _mapper.Map<List<OwnerDto>>(owners);  //i get this error here
			return Ok(ownerListDto);
		}
			
	}
}
