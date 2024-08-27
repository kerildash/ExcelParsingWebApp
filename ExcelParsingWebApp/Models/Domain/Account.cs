namespace ExcelParsingWebApp.Models.Domain;

public record Account
{
    public required int Id { get; set; }
    public required decimal IncomingBalanceAssets { get; init; }
	public required decimal IncomingBalanceLiabilities { get; init; }
	public required decimal Debit { get; init; }
	public required decimal Credit { get; init; }
}
