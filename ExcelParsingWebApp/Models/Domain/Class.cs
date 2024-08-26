namespace ExcelParsingWebApp.Models.Domain;

public record Class
{
	public required Guid Id { get; init; }
    public required Guid SheetId { get; init; }
    public required string ClassName { get; init; }
}
