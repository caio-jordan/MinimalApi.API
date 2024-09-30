using Microsoft.EntityFrameworkCore;
using MinimalApi.API.DbContexts;
using MinimalApi.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PratoDbContext>(
    o => o.UseSqlite(builder.Configuration["ConnectionStrings:PratoDbConStr"])
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddProblemDetails();

var app = builder.Build();

if  (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.RegistrePratosEndpoints();
app.RegistreIngredientesEndpoints();

app.Run();