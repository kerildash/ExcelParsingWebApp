using AutoMapper;
using ExcelParsingWebApp.Database.Repositories;
using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Models.Database;
using Microsoft.AspNetCore.Mvc;
using ExcelParsingWebApp.Models.ViewModels;

namespace ExcelParsingWebApp.Controllers;

public class SheetController(SheetRepository repository, IMapper mapper) : Controller
{
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		List<Sheet> sheets = mapper.Map<List<Sheet>>(await repository.GetAllAsync());
		return View(model: sheets);
	}
	[HttpGet]
	public async Task<IActionResult> Get([FromQuery]Guid id)
	{
		SheetDto dto = await repository.GetAsync(id);
		SheetViewModel sheet = mapper.Map<SheetViewModel>(dto);
		foreach (ClassViewModel c in sheet.Classes)
		{
			c.Accounts = c.Accounts.OrderBy(a => a.Id).ToList();
		}
		sheet.Classes = sheet.Classes.OrderBy(c => c.ClassName).ToList();
		return View(sheet);
	}
}
