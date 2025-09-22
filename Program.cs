using Microsoft.EntityFrameworkCore;
using RandomUserImporter.Models;
using RandomUserImporter.Repositories;
using RandomUserImporter.Service;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Configuração do banco PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Injeta dependências
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRandomUserService, RandomUserService>();

// 🔹 Configura HttpClient para consumir API RandomUser
builder.Services.AddHttpClient("randomuser", client =>
{
    client.BaseAddress = new Uri("https://randomuser.me/api/");
});

// 🔹 Habilita CORS (permite qualquer origem)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Configuração do pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔹 Ativa arquivos estáticos (wwwroot/index.html)
app.UseDefaultFiles();   // procura por index.html
app.UseStaticFiles();    // habilita servir JS, CSS, imagens

// 🔹 Ativa CORS
app.UseCors("AllowAll");

app.UseAuthorization();

// 🔹 Mapeia Controllers (APIs)
app.MapControllers();

// 🔹 Sempre por último
app.Run();
