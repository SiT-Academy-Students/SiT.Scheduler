using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SiT.Scheduler.API.Configuration;
using SiT.Scheduler.Core.Configuration;
using SiT.Scheduler.Data.Configuration;
using SiT.Scheduler.StorageManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

var databaseConfiguration = builder.Configuration.GetSection(DatabaseConfiguration.Section).Get<DatabaseConfiguration>();
builder.Services.SetupDatabase(databaseConfiguration);

var storageManagementConfiguration = builder.Configuration.GetSection(StorageManagementConfiguration.Section).Get<StorageManagementConfiguration>();
builder.Services.SetupStorageManagement(storageManagementConfiguration, builder.Configuration);

builder.Services.RegisterServices();
builder.Services.RegisterFactories();

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();