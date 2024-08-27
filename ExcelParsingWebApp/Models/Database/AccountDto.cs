using Microsoft.EntityFrameworkCore;

namespace ExcelParsingWebApp.Models.Database;

[PrimaryKey(nameof(Id), nameof(SheetId))]
public record AccountDto
{
    public required int Id { get; set; }
    public required Guid SheetId { get; init; }
    public required Guid ClassId { get; init; }
    public required decimal IncomingBalanceAssets { get; init; }
	public required decimal IncomingBalanceLiabilities { get; init; }
	public required decimal Debit { get; init; }
	public required decimal Credit { get; init; }
}
