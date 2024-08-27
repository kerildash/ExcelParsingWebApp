using ExcelParsingWebApp.Models.Domain;
using ExcelParsingWebApp.Models;


namespace ExcelParsingWebApp.Services;

public interface IWorksheetReader
{
    Task CreateAsync(string fileName);
    Task<string?> GetWorksheetNameAsync();
    Task<Sheet> ReadHeaderAsync();
    ContentType CheckContentType();
    Class ReadClass();
    Account? ReadAccount();
    bool GoNextRaw();
    void Close();
}
