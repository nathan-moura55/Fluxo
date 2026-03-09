using Microsoft.EntityFrameworkCore;
using Estoque.Repositorio.Data;
using Estoque.Repositorio;
using Estoque.Servicos;
using Estoque.Dominio.Interfaces;
using Estoque.Dominio.Models;
using DotNetEnv;

DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

builder.Services.AddControllers();

builder.Services.AddDbContext<EstoqueDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorioSql>();
builder.Services.AddScoped<ILogRepositorio, LogRepositorioSql>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorioSql>();
builder.Services.AddScoped<IControleDeEstoque, ControleDeEstoque>();
builder.Services.AddScoped<ISessaoUsuario, SessaoUsuarioMock>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();