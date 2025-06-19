using CollectR.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectR.Api.Extensions;

internal static class ApplicationBuilderExtensions
{
    internal static IApplicationBuilder EnsureDatabaseCreated(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.Migrate();

        return builder;
    }
}
