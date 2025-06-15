using CollectR.Api.Endpoints;
using CollectR.Api.Middleware;
using CollectR.Application;
using CollectR.Infrastructure;
using CollectR.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services.AddApplication()
    .AddInfrastructure(webRootPath => // pass webhostenv https://github.com/jasontaylordev/NorthwindTraders/blob/master/Src/WebUI/Startup.cs
    {
        var env = webRootPath.GetRequiredService<IWebHostEnvironment>();
        return env.WebRootPath;
    })
    .AddPersistence(builder.Configuration);

builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandlingMiddleware();

app.UseHttpsRedirection();

app.MapCategoryEndpoints();
app.MapCollectibleEndpoints();
app.MapCollectionEndpoints();
app.MapTagEndpoints();

app.MapHealthChecks("/health");

app.Run();
