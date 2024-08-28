using ExcelParsingWebApp.Models.Database;

namespace ExcelParsingWebApp.Models.ViewModels;

public record ClassViewModel
{
	public required Guid Id { get; init; }
	public required Guid SheetId { get; init; }
	public required string ClassName { get; init; }

	public virtual List<AccountViewModel>? Accounts { get; set; }
	public virtual SheetViewModel? Sheet { get; set; }
}