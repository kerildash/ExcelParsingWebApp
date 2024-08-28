using ExcelParsingWebApp.Models.Database;

namespace ExcelParsingWebApp.Models.ViewModels;

public record AccountViewModel
{
	public required int Id { get; set; }
	public required Guid ClassId { get; init; }
	public required decimal IncomingBalanceAssets { get; init; }
	public required decimal IncomingBalanceLiabilities { get; init; }
	public required decimal Debit { get; init; }
	public required decimal Credit { get; init; }
	public required decimal OutgoingBalanceAssets { get; init; }
	public required decimal OutgoingBalanceLiabilities { get; init; }
	public required bool IsClusterSum { get; init; }

	public virtual ClassViewModel? Class { get; set; }
}
