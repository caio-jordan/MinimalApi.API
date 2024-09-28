using System.Buffers;
using Microsoft.EntityFrameworkCore;
using MinimalApi.API.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PratoDbContext>(
    o => o.UseSqlite(builder.Configuration["ConnectionStrings:PratoDbConStr"])
);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();