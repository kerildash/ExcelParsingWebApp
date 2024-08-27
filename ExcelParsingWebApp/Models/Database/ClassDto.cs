namespace ExcelParsingWebApp.Models.Database;

public record ClassDto
{
	public required Guid Id { get; init; }
    public required Guid SheetId { get; init; }
    public required string ClassName { get; init; }
}
