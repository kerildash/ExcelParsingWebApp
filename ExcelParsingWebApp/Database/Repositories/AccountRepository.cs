using ExcelParsingWebApp.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace ExcelParsingWebApp.Database.Repositories;

public class AccountRepository(AppDbContext context)
{
    public async Task<ICollection<AccountDto>> GetByClassIdAsync(Guid classId)
    {
        if (!await context.Classes.AnyAsync(c => c.Id == classId))
        {
            throw new ArgumentException($"Class entity with id {classId} not found");
        }
        return await context.Accounts.Where(a => a.ClassId == classId).ToListAsync();
    }
    public async Task CreateAsync(AccountDto c)
    {
        await context.AddAsync(c);
        await SaveAsync();
    }
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
