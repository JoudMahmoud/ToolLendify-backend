using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ToolLendify.Application.DTOs;
using ToolLendify.Domain.Entities;

namespace ToolLendify.Application.Automapper
{
	public class MappingProfile:Profile
	{
		public MappingProfile()
		{
			//CreateMap<Source,destination>

			CreateMap<Tool, ToolDto>()
				.ForMember(d => d.CategoryName, o => o.MapFrom(t => t.Category.Name))
				.ForMember(d => d.OwnerID, opt => opt.MapFrom(src => src.Owner.UserName));


			CreateMap<ToolDto, Tool>()
				.ForMember(t => t.CategoryID, opt => opt.Ignore()) //why here make id Ignore can you explain this for me
				.ForMember(t => t.Category, opt => opt.Ignore()) //and here to 
				.AfterMap((dto, tool) => {
					if (!string.IsNullOrEmpty(dto.CategoryName)) {
						tool.Category = new Category { Name = dto.CategoryName };
					}
				});

			CreateMap<CategoryDto, Category>();
			CreateMap<Category, CategoryDto>();

			CreateMap<Owner, UserDto>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.UserName))
				.ForMember(d => d.ImageUrl, o => o.MapFrom(s => s.ImageUrl));
			//Severity	Code	Description	Project	File	Line	Suppression State	Details	Error(active)  CS1061  'OwnerDto' does not contain a definition for 'Id' and no accessible extension method 'Id' accepting a first argument of type 'OwnerDto' could be found(are you missing a using directive or an assembly reference ?)	ToolLendify.Application E:\iti\0 - project\ToolLendify\ToolLendify - separate - projects\ToolLendify - Backend\ToolLendify.Application\Automapper\MappingProfile.cs 44


			CreateMap<User, UserDto>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName));

			CreateMap<AddressDto, Address>();
			CreateMap<Address, AddressDto>();
		}

	}
}