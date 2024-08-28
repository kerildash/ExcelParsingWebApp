using AutoMapper;
using ExcelParsingWebApp.Models.Database;
using ExcelParsingWebApp.Models.ViewModels;

namespace ExcelParsingWebApp.Mapping.Resolvers;

public class AccountOutgoingLiabilitiesResolver : IValueResolver<AccountDto, AccountViewModel, decimal>
{
public decimal Resolve(AccountDto dto, AccountViewModel vm, decimal assets, ResolutionContext context)
{
	if (dto.IncomingBalanceLiabilities == 0) return 0;
	return dto.IncomingBalanceLiabilities - dto.Debit + dto.Credit;
}
}
