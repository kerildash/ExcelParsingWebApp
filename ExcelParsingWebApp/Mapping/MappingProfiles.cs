using AutoMapper;
using Azure;
using ExcelParsingWebApp.Models.Database;
using ExcelParsingWebApp.Models.Domain;

namespace ExcelParsingWebApp.Mapping;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{

		CreateMap<SheetDto, Sheet>();
	}
}
