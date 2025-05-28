using CollectR.Api.Endpoints;
using CollectR.Application;
using CollectR.Infrastructure;
using CollectR.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(
    builder.Configuration,
    webRootPath => // pass webhostenv https://github.com/jasontaylordev/NorthwindTraders/blob/master/Src/WebUI/Startup.cs
    {
        var env = webRootPath.GetRequiredService<IWebHostEnvironment>();
        return env.WebRootPath;
    }
);

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

app.MapCategoryEndpoints();
app.MapCollectibleEndpoints();
app.MapCollectionEndpoints();
app.MapTagEndpoints();

app.MapHealthChecks("/health");

app.Run();
