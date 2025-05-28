using CollectR.Application.Contracts.Persistence;
using CollectR.Application.Contracts.Services;
using CollectR.Infrastructure.Persistence;
using CollectR.Infrastructure.Persistence.Repositories;
using CollectR.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CollectR.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, Func<IServiceProvider, string> webRootPath)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICollectibleRepository, CollectibleRespository>();
        services.AddScoped<ICollectionRepository, CollectionRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        services.AddScoped<IFileService, FileService>(provider =>
        {
            var path = webRootPath(provider);
            return new FileService(path);
        });

        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("ApplicationDbContext");
            options.UseSqlite(connectionString);
        });

        return services;
    }
}
