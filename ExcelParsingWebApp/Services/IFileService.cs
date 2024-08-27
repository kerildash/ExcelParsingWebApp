using ExcelParsingWebApp.Models.ViewModels;

namespace ExcelParsingWebApp.Services;

public interface IFileService
{
    Task<string> CreateAsync(UploadViewModel upload);
}