using CollectR.Application.Contracts.Services;
using CollectR.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CollectR.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IExportService, ExportService>();

        services.AddScoped<IImportService, ImportService>();

        services.AddScoped<IFileService, FileService>();

        return services;
    }
}
