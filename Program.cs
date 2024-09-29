using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.API.DbContexts;
using MinimalApi.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PratoDbContext>(
    o => o.UseSqlite(builder.Configuration["ConnectionStrings:PratoDbConStr"])
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapGet("/", () => "I'm Fine!");

app.MapGet("/pratos", async Task<Results<NoContent, Ok<IEnumerable<PratoDTO>>>>
 (PratoDbContext pratoDbContext,
 IMapper mapper,
 [FromQuery(Name = "name")] string? pratoNome) =>
     {
         var pratos = mapper.Map<IEnumerable<PratoDTO>>( await pratoDbContext.Pratos.Where(x => pratoNome == null || x.Nome.ToLower().Contains(pratoNome.ToLower())).ToListAsync());

         if (!pratos.Any())
         {
             return TypedResults.NoContent();
         }
         return TypedResults.Ok(pratos);
     });

app.MapGet("/pratos/{pratosId:int}/ingredientes", async
 (PratoDbContext pratoDbContext,
 IMapper mapper,
 int pratosId) =>
{
    return mapper.Map<IEnumerable<IngredienteDTO>>((await pratoDbContext.Pratos.Include(prato => prato.Ingredientes)
    .FirstOrDefaultAsync(prato => prato.Id == pratosId))?.Ingredientes);
});

app.MapGet("/prato/{id:int}", async
(PratoDbContext pratoDbContext,
IMapper mapper,
 int pratoId) =>
    {
        return mapper.Map<PratoDTO>(await pratoDbContext.Pratos.FirstOrDefaultAsync(x => x.Id == pratoId));
    });

app.Run();