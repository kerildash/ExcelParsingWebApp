using Microsoft.EntityFrameworkCore;

namespace ExcelParsingWebApp.Models.Database;
[Index(nameof(ClassName))]
public record ClassDto
{
	public required Guid Id { get; init; }
	public required Guid SheetId { get; init; }
	public required string ClassName { get; init; }

	public virtual ICollection<AccountDto>? Accounts { get; set; }
	public virtual SheetDto? Sheet { get; set; }
}
