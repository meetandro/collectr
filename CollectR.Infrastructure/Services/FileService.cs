﻿using CollectR.Application.Contracts.Services;
using CollectR.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CollectR.Infrastructure.Services;

public sealed class FileService(IOptions<ImageRoot> root) : IFileService
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
        
        string fullPath = Path.Combine(folderPath, fileName);
        
        Directory.CreateDirectory(folderPath);

        await using var stream = File.Create(fullPath);
        
        await file.CopyToAsync(stream);

        return fileName;
    }

    public bool DeleteFile(string fileName)
    {
        fileName = fileName.TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        
        string fullPath = Path.Combine(root.Value.Path, fileName);

        if (!File.Exists(fullPath))
        {
            return false;
        }
        
        File.Delete(fullPath);
        
        return true;

    }
}
