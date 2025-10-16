using Medilink.Context;
using Medilink.Interfaces;
using Medilink.Services;
using Microsoft.EntityFrameworkCore;
//using DoNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MedilinkDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Medilink API",
        Version = "v1",
        Description = "API para gestión de consultas médicas y médicos."
    });
});

builder.Services.AddScoped<IConsultaMedicaService, ConsultaMedicaService>();
builder.Services.AddScoped<IMedicoService,MedicoService>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
