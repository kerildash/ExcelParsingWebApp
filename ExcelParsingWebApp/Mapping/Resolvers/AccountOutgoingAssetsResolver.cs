using AutoMapper;
using ExcelParsingWebApp.Models.Database;
using ExcelParsingWebApp.Models.ViewModels;

namespace ExcelParsingWebApp.Mapping.Resolvers;

public class AccountOutgoingAssetsResolver : IValueResolver<AccountDto, AccountViewModel, decimal>
{
	public decimal Resolve(AccountDto dto, AccountViewModel vm, decimal assets, ResolutionContext context)
	{
		if (dto.IncomingBalanceAssets == 0) return 0;
		return dto.IncomingBalanceAssets + dto.Debit - dto.Credit;
	}
}
