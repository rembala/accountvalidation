using AccountBusinessLayer.Helpers;
using AccountBusinessLayer.Helpers.Interfaces;
using AccountBusinessLayer.Validations;
using AccountBusinessLayer.Validations.Interfaces;
using AccountPresentationLayer.Handlers;
using AccountPresentationLayer.Handlers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFileAccountHelper, FileAccountHelper>();
builder.Services.AddScoped<IAccountHandler, AccountHandler>();
builder.Services.AddScoped<IFileAccountMainValidator, FileAccountMainValidator>();
builder.Services.AddScoped<IFileAccountNumberValidator, FileAccountNumberValidator>();
builder.Services.AddScoped<IFileAccountNameValidator, FileAccountNameValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
