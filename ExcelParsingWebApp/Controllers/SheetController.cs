using AutoMapper;
using ExcelParsingWebApp.Database.Repositories;
using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Models.Database;
using Microsoft.AspNetCore.Mvc;

namespace ExcelParsingWebApp.Controllers
{
	public class SheetController(SheetRepository repository, IMapper mapper) : Controller
	{
		public async Task<IActionResult> Index()
		{
			List<SheetDto> sheetsDto = await repository.GetAllAsync();
			List<Sheet> sheets = mapper.Map<List<Sheet>>(sheetsDto);
			return View(model: sheets, viewName: "Index");
		}
		public IActionResult Get(Guid id)
		{
			return Ok(id.ToString());//View();
		}
	}
}
