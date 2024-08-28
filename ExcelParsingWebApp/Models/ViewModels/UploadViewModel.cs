using System.ComponentModel.DataAnnotations;

namespace ExcelParsingWebApp.Models.ViewModels;

public class UploadViewModel
{
    [Required(ErrorMessage = "Сначала выберите файл")]
    public required IFormFile File { get; set; }
}
