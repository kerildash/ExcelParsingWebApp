using AutoMapper;
using ExcelParsingWebApp.Services;
using ExcelParsingWebApp.Database.Repositories;
using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Models.Database;
using Microsoft.AspNetCore.Mvc;
using ExcelParsingWebApp.Models.ViewModels;

namespace ExcelParsingWebApp.Controllers;

public class SheetController(SheetRepository repository, IMapper mapper, RowCalculatingService service) : Controller
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
		sheet = service.AddResultingAccounts(sheet);
		
		return View(sheet);
	}
}
