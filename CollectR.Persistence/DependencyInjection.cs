using CollectR.Application.Contracts.Persistence;
using CollectR.Persistence.Interceptors;
using CollectR.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CollectR.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICollectibleRepository, CollectibleRepository>();
        services.AddScoped<ICollectionRepository, CollectionRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        services.AddSingleton<AuditableEntityInterceptor>();

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>()
        );

        services.AddDbContext<ApplicationDbContext>(
            (provider, options) =>
            {
                var connectionString = configuration.GetConnectionString("ApplicationDbContext");
                options
                    .UseSqlite(connectionString)
                    .AddInterceptors(provider.GetRequiredService<AuditableEntityInterceptor>());
            }
        );

        return services;
    }
}
