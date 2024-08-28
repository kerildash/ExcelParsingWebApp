using ExcelParsingWebApp.Models.Database;

namespace ExcelParsingWebApp.Models.ViewModels;

public record AccountViewModel
{
	public required int Id { get; set; }
	public required Guid ClassId { get; set; }
	public required decimal IncomingBalanceAssets { get; set; }
	public required decimal IncomingBalanceLiabilities { get; set; }
	public required decimal Debit { get; set; }
	public required decimal Credit { get; set; }
	public required decimal OutgoingBalanceAssets { get; set; }
	public required decimal OutgoingBalanceLiabilities { get; set; }
	public required bool IsClusterSum { get; set; }

	public virtual ClassViewModel? Class { get; set; }

	public static AccountViewModel operator +(AccountViewModel a, AccountViewModel b)
		=> new AccountViewModel
		{
			Id = a.Id,
			ClassId = a.ClassId,
			IncomingBalanceAssets = a.IncomingBalanceAssets + b.IncomingBalanceAssets,
			IncomingBalanceLiabilities = a.IncomingBalanceLiabilities + b.IncomingBalanceLiabilities,
			Debit = a.Debit + b.Debit,
			Credit = a.Credit + b.Credit,
			OutgoingBalanceAssets = a.OutgoingBalanceAssets + b.OutgoingBalanceAssets,
			OutgoingBalanceLiabilities = a.OutgoingBalanceLiabilities + b.OutgoingBalanceLiabilities,
			IsClusterSum = true,
			Class = a.Class,
		};
}
