using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Serilog;
using TestApplication;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, Configuration) =>
{
    Configuration.MinimumLevel.Information()
    .WriteTo.Console();
}
);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDependencies(builder.Configuration);
//builder.Services.AddOpenApi();
var webHostEnvironment = builder.Environment;

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
  //  app.MapOpenApi();
 
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(); // ????? wwwroot ??? ?????

app.UseStaticFiles(); // ????? wwwroot ??? ?????

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(webHostEnvironment.WebRootPath, "productImage")),
    RequestPath = "/productImage"
});

app.MapControllers();

app.Run();
