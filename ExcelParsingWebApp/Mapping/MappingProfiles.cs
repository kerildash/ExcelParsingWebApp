using AutoMapper;
using Azure;
using ExcelParsingWebApp.Mapping.Resolvers;
using ExcelParsingWebApp.Models.Database;
using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Models.ViewModels;

namespace ExcelParsingWebApp.Mapping;

public class MappingProfiles : Profile
{
	public MappingProfiles()
	{

		CreateMap<SheetDto, Sheet>();

		CreateMap<AccountDto, AccountViewModel>()
			.ForMember(dest => dest.OutgoingBalanceAssets, opt => opt.MapFrom<AccountOutgoingAssetsResolver>())
			.ForMember(dest => dest.OutgoingBalanceLiabilities, opt => opt.MapFrom<AccountOutgoingLiabilitiesResolver>());


		CreateMap<SheetDto, SheetViewModel>();
		CreateMap<ClassDto, ClassViewModel>();
	}
}
