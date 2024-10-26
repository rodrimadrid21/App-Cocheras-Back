using Data.context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Inyección de dependencias para Repositorios
builder.Services.AddScoped<IUserRepository>();
builder.Services.AddScoped<ITarifaRepository>();
builder.Services.AddScoped<IEstacionamientoRepository>();
builder.Services.AddScoped<ICocheraRepository>();
// Inyección de dependencias para Servicios
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TarifaService>();
builder.Services.AddScoped<EstacionamientoService>();
builder.Services.AddScoped<CocheraService>();

builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger para incluir JWT
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("EstacionamientoAustralApi", new OpenApiSecurityScheme() //Esto va a permitir usar swagger con el token.
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
                    Id = "EstacionamientoAustralApi" } //Tiene que coincidir con el id seteado arriba en la definición
                }
            , new List<string>() }
    });
});
// Configuración de autenticación con JWT
builder.Services.AddAuthentication("Bearer") //"Bearer" es el tipo de auntenticación que tenemos que elegir después en PostMan para pasarle el token
    .AddJwtBearer(options => //Acá definimos la configuración de la autenticación. le decimos qué cosas queremos comprobar. La fecha de expiración se valida por defecto.
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
    }
);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
