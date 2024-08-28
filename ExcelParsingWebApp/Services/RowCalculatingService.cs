using ExcelParsingWebApp.Models.ViewModels;

namespace ExcelParsingWebApp.Services;

public class RowCalculatingService
{
	public SheetViewModel AddResultingAccounts(SheetViewModel sheet)
	{
		foreach (ClassViewModel c in sheet.Classes)
		{
			c.Accounts = CalculateResultingRowsForGroup(c.Accounts);

			
		}
		sheet.Classes = sheet.Classes.OrderBy(c => c.ClassName).ToList();
		AccountViewModel sheetResult = new AccountViewModel
		{
			Id = 0,
			ClassId = Guid.Empty,
			IncomingBalanceAssets = 0,
			IncomingBalanceLiabilities = 0,
			Debit = 0,
			Credit = 0,
			OutgoingBalanceAssets = 0,
			OutgoingBalanceLiabilities = 0,
			IsClusterSum = true,
		};
		
		foreach (ClassViewModel c in sheet.Classes)
		{
			sheetResult += c.Accounts.First(a => a.Id == 999);
		}
		sheet.Classes.Last().Accounts.Add(sheetResult);
		return sheet;
	}
	private List<AccountViewModel> CalculateResultingRowsForGroup(List<AccountViewModel> accounts)
	{
		var groups = accounts.GroupBy(x => x.Id / 100);
		AccountViewModel classResult = new AccountViewModel
		{
			Id = 999,
			ClassId = accounts.First().ClassId,
			IncomingBalanceAssets = 0,
			IncomingBalanceLiabilities = 0,
			Debit = 0,
			Credit = 0,
			OutgoingBalanceAssets = 0,
			OutgoingBalanceLiabilities = 0,
			IsClusterSum = true,
		};
		foreach (var group in groups)
		{
			AccountViewModel groupResult = new AccountViewModel
			{
				Id = 0,
				ClassId = accounts.First().ClassId,
				IncomingBalanceAssets = 0,
				IncomingBalanceLiabilities = 0,
				Debit = 0,
				Credit = 0,
				OutgoingBalanceAssets = 0,
				OutgoingBalanceLiabilities = 0,
				IsClusterSum = true,
			};
			foreach (var row in group)
			{
				groupResult += row;
				groupResult.Id = row.Id / 100;
			}
			accounts.Add(groupResult);
			classResult += groupResult;
		}
		accounts.Add(classResult);
		accounts = accounts.OrderBy(a => a.Id.ToString()).ToList();
		return accounts;
	}
}
