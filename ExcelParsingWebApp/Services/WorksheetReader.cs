using ExcelDataReader;
using ExcelParsingWebApp.Models.Domain;

namespace ExcelParsingWebApp.Services;

public class WorksheetReader : IWorksheetReader
{
    private IExcelDataReader Reader { get; set; }
 
    public async Task Read()
    {
        
    }
    public async Task CreateAsync(string fileName)
    {
        Stream stream = File.OpenRead(fileName);
        Reader = ExcelReaderFactory.CreateReader(stream);
    }
    public async Task<string?> GetWorksheetNameAsync()
    {
        return Reader.Name;
    }
    public async Task<Sheet> ReadHeaderAsync()
    {
        Reader.Read();
        string bankName = Reader.GetString(0);
        Reader.Read();
        string title = Reader.GetString(0);
        Reader.Read();
        string periodInfo = Reader.GetString(0);
        Reader.Read();
        string additionalInfo = Reader.GetString(0);
        Reader.Read();
        Reader.Read();
        DateTime date = Reader.GetDateTime(0);
        string currency = Reader.GetString(6);
        return new Sheet
        {
            Id = Guid.NewGuid(),
            BankName = bankName,
            SheetTitle = title,
            PeriodInfo = periodInfo,
            AdditionalInfo = additionalInfo,
            Date = date,
            Currency = currency
        };
    }
    public void Close()
    {
        Reader.Close();
    }
}
