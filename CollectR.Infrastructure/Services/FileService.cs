using CollectR.Application.Contracts.Services;
using CollectR.Infrastructure.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CollectR.Infrastructure.Services;

public class FileService(IOptions<ImageRoot> root) : IFileService
{
    public async Task<byte[]> ConvertToByteArrayAsync(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }

    public async Task<string> SaveFileInFolderAsync(IFormFile file, string folder)
    {
        string fileExtension = Path.GetExtension(file.FileName);
        string fileName = $"{Guid.NewGuid()}{fileExtension}";

        string folderPath = Path.Combine("wwwroot", folder);
        Directory.CreateDirectory(folderPath);

        string fullPath = Path.Combine(folderPath, fileName);
        using (var stream = File.Create(fullPath))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }

    public void DeleteFileInFolder(string fileName, string folder)
    {
        string fullPath = root.Value.Path + fileName;
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}
