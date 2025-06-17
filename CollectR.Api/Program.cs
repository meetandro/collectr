using CollectR.Api.Endpoints;
using CollectR.Api.Middleware;
using CollectR.Api.Options;
using CollectR.Application;
using CollectR.Infrastructure;
using CollectR.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPersistence(builder.Configuration);

builder.Services.AddHealthChecks();

builder.Services.ConfigureOptions<ImageRootSetup>();

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
