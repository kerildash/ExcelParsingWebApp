using ExcelParsingWebApp.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace ExcelParsingWebApp.Database;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<AccountDto> Accounts { get; set; }
	public DbSet<ClassDto> Classes { get; set; }
	public DbSet<SheetDto> Sheets { get; set; }
}