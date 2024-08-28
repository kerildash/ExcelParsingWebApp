using ExcelParsingWebApp.Models.Database;

namespace ExcelParsingWebApp.Models.ViewModels;

public record SheetViewModel
{
	public required Guid Id { get; init; }
	public required string SheetTitle { get; init; }
	public required string BankName { get; init; }
	public required string PeriodInfo { get; init; }
	public required string AdditionalInfo { get; init; }
	public required DateTime Date { get; init; }
	public required string Currency { get; init; }

	public virtual ICollection<ClassViewModel>? Classes { get; set; }

}
