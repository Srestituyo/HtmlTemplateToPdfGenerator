using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using PdfGenerator.Application;
using PdfGenerator.Infrastructure;
using PdfGenerator.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add layer dependencies
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddControllers().AddFluentValidation();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "Html tsemplate to PDF generator", Version = "v1" });

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("allow-all", builder =>
    {
        builder.SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .Build();
    });
});

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseErrorHandlerMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();