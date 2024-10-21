

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ventasG.Models;

//public void ConfigureServices(IServiceCollection services)
//{
//    services.AddControllers();

//    // Configura la cadena de conexión desde appsettings.json
//    services.AddDbContext<ApplicationDbContext>(options =>
//        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

//    // Configura Swagger
//    services.AddSwaggerGen();




var builder = WebApplication.CreateBuilder(args);

// Agregar el DbContext para SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar servicios y Swagger
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();















//using Microsoft.Extensions.Configuration;

//var builder = WebApplication.CreateBuilder(args);




//// Add services to the container.

//builder.Services.AddControllers();

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
