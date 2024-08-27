using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace ExcelParsingWebApp.Database.Repositories;

public class ClassRepository(AppDbContext context)
{
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await context.Classes.AnyAsync(c => c.Id == id);
    }
    public async Task<ClassDto> GetAsync(Guid id)
    {
        if (!await ExistsAsync(id))
        {
            throw new ArgumentException($"Class entity with id {id} not found");
        }
        return await context.Classes.FirstAsync(c => c.Id == id);
    }
    public async Task<ICollection<ClassDto>> GetBySheetIdAsync(Guid sheetId)
    {
        if (!await context.Sheets.AnyAsync(sh => sh.Id == sheetId))
        {
            throw new ArgumentException($"Sheet entity with id {sheetId} not found");
        }
        return await context.Classes.Where(c => c.SheetId == sheetId).ToListAsync();
    }
    public async Task CreateAsync(ClassDto c)
    {
        await context.AddAsync(c);
        await SaveAsync();
    }
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }

}
