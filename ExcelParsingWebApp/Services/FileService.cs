using ExcelParsingWebApp.Models.ViewModels;
using System.Text.RegularExpressions;

namespace ExcelParsingWebApp.Services;

public class FileService(IWebHostEnvironment environment) : IFileService
{
    public async Task<string> CreateAsync(UploadViewModel upload)
    {
        string name = HandleName(upload.File.FileName);
        string path = await SaveInTemporary(name, upload.File);

        return path;
    }
    private async Task<string> SaveInWebRootAsync(string name, IFormFile staticFileCreate)
    {
        string path = Path.Combine(environment.WebRootPath, @"Files", name);
        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            await staticFileCreate.CopyToAsync(fileStream);
        }
        return path;
    }
    private async Task<string> SaveInTemporary(string name, IFormFile staticFileCreate)
    {
        string path = Path.Combine(@"tmp", name);
        using (var fileStream = new FileStream(path, FileMode.Create))
        {
            await staticFileCreate.CopyToAsync(fileStream);
        }
        return path;
    }
    private string HandleName(string name)
    {
        if (!IsValidName(name))
        {
            return $"{Guid.NewGuid()}.{Path.GetExtension(name)}";
        }
        return Regex.Replace(name, @"\s+", "");
    }
    private bool IsValidName(
        string text,
        string pattern = @"^[a-zA-Z0-9](?:[a-zA-Z0-9 ._-]*[a-zA-Z0-9])?\.[a-zA-Z0-9_-]+$")
    {
        return Regex.IsMatch(text, pattern);
    }

    private bool IsValidExtension(IFormFile file)
    {
        List<string> extensions = ["xls", "xlsx"];
        string extension = GetExtension(file);
        if (extensions.Contains(extension))
        {
            return true;
        }
        return false;
    }
    private string GetExtension(IFormFile file)
    {
        return Path.GetExtension(file.FileName).Trim().ToLower();
    }


}