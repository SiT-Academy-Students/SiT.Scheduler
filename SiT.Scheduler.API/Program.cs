using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using SiT.Scheduler.API.Configuration;
using SiT.Scheduler.Core.Configuration;
using SiT.Scheduler.Data.Configuration;
using SiT.Scheduler.Organization.Configuration;
using SiT.Scheduler.StorageManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true);
builder.Services.Configure<FormOptions>(
    formOptions =>
    {
        formOptions.MultipartBodyLengthLimit = 100 * 1024 * 1024;
    });

var b2cSection = builder.Configuration.GetSection("AzureAdB2C");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(b2cSection);

builder.Services.AddAuthorization(
    options =>
    {
        var policyBuilder = new AuthorizationPolicyBuilder();
        policyBuilder.RequireAuthenticatedUser();
        policyBuilder.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
        var defaultPolicy = policyBuilder.Build();

        options.DefaultPolicy = defaultPolicy;
        options.FallbackPolicy = defaultPolicy;
    });

var databaseConfiguration = builder.Configuration.GetSection(DatabaseConfiguration.Section).Get<DatabaseConfiguration>();
builder.Services.SetupDatabase(databaseConfiguration);

var storageManagementConfiguration = builder.Configuration.GetSection(StorageManagementConfiguration.Section).Get<StorageManagementConfiguration>();
builder.Services.SetupStorageManagement(storageManagementConfiguration, builder.Configuration);

builder.Services.RegisterServices();
builder.Services.RegisterFactories();
builder.Services.ConfigureGraphConnection(builder.Configuration);

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
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.RunAsync();