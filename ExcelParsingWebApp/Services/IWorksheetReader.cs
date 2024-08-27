using ExcelParsingWebApp.Models.Domain;

namespace ExcelParsingWebApp.Services;

public interface IWorksheetReader
{
    Task Read();
    Task CreateAsync(string fileName);
    Task<string?> GetWorksheetNameAsync();
    Task<Sheet> ReadHeaderAsync();
    void Close();

}
