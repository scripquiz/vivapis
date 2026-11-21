using Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ViviliamAPI.BibleServices;

try
{
    var builder = WebApplication.CreateBuilder(args);
   
    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
    var config = new ConfigurationBuilder()
       .SetBasePath(builder.Environment.ContentRootPath)
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
       .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
       .AddEnvironmentVariables()
       .Build();

    builder.Services.AddDbContext<ViviliamDbContext>(options =>
              options.UseSqlServer(config.GetConnectionString("ViviliamConnection")));


    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IBibleService, BibleService>();

    // Configure CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder.WithOrigins("http://localhost:4200", "https://localhost:4200", "https://localhost", "http://viviliam.com", "https://viviliam.com", "http://scripquiz.com", "https://scripquiz.com")
            .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowAnyMethod());
    });

    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(config).CreateLogger();
    Log.Information("******************Starting Application******************");

    builder.Host.UseSerilog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseCors("CorsPolicy");
    app.UseStaticFiles();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Logger.Fatal(ex, "********Application terminated unexpectedly********");
}
finally
{
    Log.CloseAndFlush();
}
