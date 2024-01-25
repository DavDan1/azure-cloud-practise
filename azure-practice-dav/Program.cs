using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the Azure Storage connection string from the configuration
string tableStorageUri = builder.Configuration["ConnectionStrings:blobdav"];

if (string.IsNullOrEmpty(tableStorageUri))
{
    Console.WriteLine("Azure Storage connection string not found in configuration.");
    // Handle the missing configuration appropriately, throw an exception, log an error, etc.
    return;
}

builder.Services.AddSingleton<BlobService>();

builder.Services.AddSingleton(sp => tableStorageUri);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString") ?? throw new InvalidOperationException("Connection string 'DbConnectionString' not found.")), ServiceLifetime.Scoped);

// This line adds the CORS services to the container.
builder.Services.AddCors();

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Add logging to help diagnose the issue
    Console.WriteLine($"Retrieving tableStorageUri: {tableStorageUri}");

    // Move the retrieval of retrievedTableStorageUri inside the using block
    var retrievedTableStorageUri = services.GetRequiredService<string>();

    SeedData.Initialize(services, tableStorageUri);
}

// Now you can configure the CORS policy.
app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
