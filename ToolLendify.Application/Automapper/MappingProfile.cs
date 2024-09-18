using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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

			CreateMap<Tool, toolDto>()
				.ForMember(d => d.Model, o => o.MapFrom(t => t.Model))
				.ForMember(d => d.CategoryName, o => o.MapFrom(t => t.Category.Name));

			//CreateMap<List<Tool>, List<toolDto>>();

			CreateMap<toolDto, Tool>()
				.ForMember(t => t.Model, o => o.MapFrom(d => d.Model))
				.ForMember(t => t.OwnerID, o => o.MapFrom(d => d.OwnerID))
				.ForMember(t => t.CategoryID, opt => opt.Ignore())
				.ForMember(t => t.Category, opt => opt.Ignore())
				.AfterMap((dto, tool) => {
					if (!string.IsNullOrEmpty(dto.CategoryName)) {
						tool.Category = new Category { Name = dto.CategoryName };
					}
				});
				
		}

	}
}