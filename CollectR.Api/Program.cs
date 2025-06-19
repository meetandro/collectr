using Asp.Versioning;
using CollectR.Api.Endpoints;
using CollectR.Api.Extensions;
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

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.EnsureDatabaseCreated();

app.UseExceptionHandlingMiddleware();

app.UseHttpsRedirection();

var apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();
var versionedGroup = app
    .MapGroup("api/v{apiVersion:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

versionedGroup.MapCategoryEndpoints();
versionedGroup.MapCollectibleEndpoints();
versionedGroup.MapCollectionEndpoints();
versionedGroup.MapTagEndpoints();

app.MapHealthChecks("/health");

app.Run();
