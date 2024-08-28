﻿using ExcelParsingWebApp.Models.Database;
using ExcelParsingWebApp.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExcelParsingWebApp.Database.Repositories;

public class SheetRepository(AppDbContext context)
{
    public async Task<SheetDto> GetAsync(Guid Id)
    {
        if (!await context.Sheets.AnyAsync(sh => sh.Id == Id))
        {
            throw new ArgumentException($"Sheet with id {Id} not found");
        }
        return await context.Sheets.FirstAsync(sh => sh.Id == Id);
    }
    public async Task<ICollection<SheetDto>> GetAllAsync(Guid Id)
    {
        
        return await context.Sheets.ToListAsync();
    }
    public async Task CreateAsync(SheetDto sh)
    {
        await context.AddAsync(sh);
        await SaveAsync();
    }
    public async Task SaveAsync()
    {
        await context.SaveChangesAsync();
    }
}
