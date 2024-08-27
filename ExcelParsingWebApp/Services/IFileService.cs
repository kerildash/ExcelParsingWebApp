using ExcelParsingWebApp.Models;

namespace ExcelParsingWebApp.Services;

public interface IFileService
{
    Task<string> CreateAsync(UploadViewModel upload);
}