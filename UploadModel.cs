using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class UploadModel : PageModel
{
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(List<IFormFile> files)
    {
        var uploadsDirectory = Path.Combine("wwwroot", "Files");

        if (!Directory.Exists(uploadsDirectory))
        {
            Directory.CreateDirectory(uploadsDirectory);
        }

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                var filePath = Path.Combine(uploadsDirectory, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
        }

        return RedirectToPage("/Index"); // або куди вам потрібно перенаправити після завантаження файлу
    }
}
