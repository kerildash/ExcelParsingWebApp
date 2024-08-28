using AutoMapper;
using ExcelParsingWebApp.Database.Repositories;
using ExcelParsingWebApp.Models;
using ExcelParsingWebApp.Models.Database;
using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Models.ViewModels;

namespace ExcelParsingWebApp.Services;

public class ExcelToSqlService(

	IFileService fileService,
	IWorksheetReader worksheetReader,
	SheetRepository sheetRepo,
	ClassRepository classRepo,
	AccountRepository accountRepo,
	IMapper mapper)
{
	public async Task<Guid> UploadAndPushToDb(UploadViewModel model)
	{
		string filePath = await fileService.CreateAsync(model);

		await worksheetReader.CreateAsync(filePath);
		Sheet sheet = await worksheetReader.ReadHeaderAsync();
		SheetDto dto = mapper.Map<SheetDto>(sheet);
		await sheetRepo.CreateAsync(dto);

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
		return sheet.Id;
	}
}
