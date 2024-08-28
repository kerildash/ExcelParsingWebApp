using ExcelParsingWebApp.Models.Domain;

namespace ExcelParsingWebApp.Models.Database;

public record ClassDto
{
	public required Guid Id { get; init; }
    public required Guid SheetId { get; init; }
    public required string ClassName { get; init; }

    public ICollection<AccountDto>? Accounts { get; set; }
    public SheetDto? Sheet { get; set; }
}
