using ExcelParsingWebApp.Database.Repositories;
using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Models.ViewModels;
using ExcelParsingWebApp.Models.Database;
using ExcelParsingWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using ExcelParsingWebApp.Models;
using AutoMapper;

namespace ExcelParsingWebApp.Controllers;

public class FileUploadController(
	ExcelToSqlService service
	) : Controller
{
	[HttpGet]
	public IActionResult Index()
	{
		return Upload();
	}
	[HttpGet]
	public ActionResult Upload()
	{
		return View();
	}
	[HttpPost]
	public async Task<IActionResult> Upload(UploadViewModel model)
	{
		if (!ModelState.IsValid)
		{
			return View(model);
		}
		if (model.File == null || model.File.Length == 0)
		{
			return BadRequest("No file selected.");
		}
		Guid sheetId = Guid.Empty;
		try
		{
			sheetId = await service.UploadAndPushToDb(model);

		}
		catch
		{
			ModelState.AddModelError("File", "Выбран неподходящий файл для загрузки");
			return View(model);
		}

		return RedirectToAction("Get", "Sheet", new { id = sheetId });

	}

}
