using Data.context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendLocalhost",
        builder => builder
            .WithOrigins("http://localhost:4200") // URL del frontend
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddControllers();

// Inyección de dependencias para Repositorios y Servicios
builder.Services.AddScoped<IUserRepository>();
builder.Services.AddScoped<ITarifaRepository>();
builder.Services.AddScoped<IEstacionamientoRepository>();
builder.Services.AddScoped<ICocheraRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TarifaService>();
builder.Services.AddScoped<EstacionamientoService>();
builder.Services.AddScoped<CocheraService>();

builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger para incluir JWT
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Acá pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            }, new List<string>()
        }
    });
});

// Configuración de autenticación con JWT
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

// Configuración de Entity Framework y base de datos
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:DBConnectionString"],
        b => b.MigrationsAssembly("EstacionamientoAustralApi")));

var app = builder.Build();

// Configuración del pipeline de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar CORS
app.UseCors("AllowFrontendLocalhost");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
