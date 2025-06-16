using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using CollectR.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CollectR.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        Func<IServiceProvider, string> webRootPath
    )
    {
        services.AddScoped<IExportService, ExportService>();

        services.AddScoped<IImportService, ImportService>();

        services.AddScoped<IFileService, FileService>(provider =>
        {
            var path = webRootPath(provider);
            return new FileService(path);
        });

        return services;
    }
}
