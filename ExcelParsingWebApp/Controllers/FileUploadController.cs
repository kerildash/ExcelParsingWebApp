using ExcelParsingWebApp.Database.Repositories;
using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Models.ViewModels;
using ExcelParsingWebApp.Models.Database;
using ExcelParsingWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using ExcelParsingWebApp.Models;

namespace ExcelParsingWebApp.Controllers
{
    public class FileUploadController(
		IFileService fileService,
		IWorksheetReader worksheetReader,
		SheetRepository sheetRepo,
		ClassRepository classRepo,
		AccountRepository accountRepo
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

				await sheetRepo.CreateAsync(
					new SheetDto
					{
						Id = sheet.Id,
						SheetTitle = sheet.SheetTitle,
						BankName = sheet.BankName,
						PeriodInfo = sheet.PeriodInfo,
						AdditionalInfo = sheet.AdditionalInfo,
						Date = sheet.Date,
						Currency = sheet.Currency
					});

				Class? c = null;
				Account? account;
				while (worksheetReader.GoNextRaw())
				{
					ContentType type = worksheetReader.CheckContentType();
					switch (type)
					{
						case ContentType.Empty:
							break;
						case ContentType.Class:
                            c = worksheetReader.ReadClass();

                            await classRepo.CreateAsync(
                                new ClassDto
                                {
                                    Id = c.Id,
                                    SheetId = sheet.Id,
                                    ClassName = c.ClassName,
                                });
							break;
						case ContentType.Account:
                            account = worksheetReader.ReadAccount();
                            if (account != null)
                            {
                                await accountRepo.CreateAsync(
                                    new AccountDto
                                    {
                                        Id = account.Id,
                                        ClassId = c.Id,
                                        IncomingBalanceAssets = account.IncomingBalanceAssets,
                                        IncomingBalanceLiabilities = account.IncomingBalanceLiabilities,
                                        Debit = account.Debit,
                                        Credit = account.Credit
                                    });
                            }
							break;
						default:
							break;
                    }

				}
                worksheetReader.Close();
                return Ok($"File uploaded successfully: {model.File.FileName}, {await worksheetReader.GetWorksheetNameAsync()}");
				
			}

            return BadRequest("No file selected.");
        }
		
	}
}
