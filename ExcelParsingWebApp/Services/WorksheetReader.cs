using ExcelDataReader;
using ExcelParsingWebApp.Models;
using ExcelParsingWebApp.Models.Domain;

namespace ExcelParsingWebApp.Services;

public class WorksheetReader : IWorksheetReader
{
    private IExcelDataReader Reader { get; set; }

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
        Reader.Read();
        Reader.Read();
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
    public ContentType CheckContentType()
    {
        if (Reader.GetValue(0) is null)
            return ContentType.Empty;
        if (Reader.GetValue(1) is null)
            return ContentType.Class;
        return ContentType.Account;
    }
    public Class ReadClass()
    {
        string name = Reader.GetString(0);
        return new Class { Id = Guid.NewGuid(), ClassName = name };
    }
    public Account? ReadAccount()
    {
        int id = 0;
        try
        {
            id = Int32.Parse(Reader.GetValue(0).ToString());
        }
        catch
        {
            return null;
        }
        if (id < 1000)
        {
            return null;
        }

        return new Account
        {
            Id = id,
            IncomingBalanceAssets = (decimal)Reader.GetDouble(1),
            IncomingBalanceLiabilities = (decimal)Reader.GetDouble(2),
            Debit = (decimal)Reader.GetDouble(3),
            Credit = (decimal)Reader.GetDouble(4),
        };
    }
    public bool GoNextRaw()
    {
        return Reader.Read();
    }
    public void Close()
    {
        Reader.Close();
    }
}
