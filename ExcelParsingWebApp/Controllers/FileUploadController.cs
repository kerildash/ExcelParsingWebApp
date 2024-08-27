using ExcelParsingWebApp.Models;
using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExcelParsingWebApp.Controllers
{
	public class FileUploadController(
		IFileService fileService,
		IWorksheetReader worksheetReader
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
            if (model.File != null && model.File.Length > 0)
            {
				string filePath = await fileService.CreateAsync(model);

                await worksheetReader.CreateAsync(filePath);
				Sheet sheet = await worksheetReader.ReadHeaderAsync();
                return Ok($"File uploaded successfully: {model.File.FileName}, {await worksheetReader.GetWorksheetNameAsync()}");
            }

            return BadRequest("No file selected.");
        }
		
	}
}
