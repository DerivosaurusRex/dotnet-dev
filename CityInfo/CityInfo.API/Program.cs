using CityInfo.API;
using CityInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Creating Serilog logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger(); ;

builder.Host.UseSerilog();


// Add services to the container.

builder.Services.AddControllers(options =>
{ options.ReturnHttpNotAcceptable = true; })
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
    

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

// Injecting custom service that we created. Provide an interface and the implementation which we want the instance of.

#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

builder.Services.AddSingleton<CitiesDataStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();