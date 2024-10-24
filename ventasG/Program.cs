

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ventasG.Models;
using Microsoft.OpenApi.Models;

//public void ConfigureServices(IServiceCollection services)
//{
//    services.AddControllers();

//    // Configura la cadena de conexión desde appsettings.json
//    services.AddDbContext<ApplicationDbContext>(options =>
//        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

//    // Configura Swagger
//    services.AddSwaggerGen();




var builder = WebApplication.CreateBuilder(args);

//  DbContext SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

//--------------------------------------------------------//

// Swagger y otros servicios >:v
builder.Services.AddControllers();
builder.Services.AddSwaggerGen( //here
    c =>
    {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    //  soporte JWT 
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = " Holis, tienes que poner Bearer y despues el token, oki? Bearer : Token, ponle asi sin comillas ni llaves, thx",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
    });




var app = builder.Build();

// Configurar Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication(); //jwt
app.UseAuthorization();

app.MapControllers();

app.Run();

