using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Models.ViewModels;
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
				//push sheet to db
				Class c;
				Account? account;
				while (worksheetReader.GoNextRaw())
				{
					if (worksheetReader.CheckContentType()== Models.ContentType.Class)
					{
						c = worksheetReader.ReadClass();
						//push to db
					}
					if (worksheetReader.CheckContentType() == Models.ContentType.Account)
					{
						account = worksheetReader.ReadAccount();
						if (account != null)
						{
							//push to db
						}
					}
				}
                worksheetReader.Close();
                return Ok($"File uploaded successfully: {model.File.FileName}, {await worksheetReader.GetWorksheetNameAsync()}");
				
			}

            return BadRequest("No file selected.");
        }
		
	}
}
