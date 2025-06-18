using Microsoft.AspNetCore.Http;

namespace CollectR.Application.Contracts.Services;

public interface IFileService
{
    Task<byte[]> ConvertToByteArrayAsync(IFormFile file);

    Task<string> SaveFileInFolderAsync(IFormFile file, string folder);

    public bool DeleteFileInFolder(string fileName, string folder);
}
