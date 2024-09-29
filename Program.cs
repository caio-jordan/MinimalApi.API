using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.API.DbContexts;
using MinimalApi.API.Entities;
using MinimalApi.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PratoDbContext>(
    o => o.UseSqlite(builder.Configuration["ConnectionStrings:PratoDbConStr"])
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapGet("/", () => "I'm Fine!");

app.MapGet("/pratos/{pratoNome}", async Task<Results<NoContent, Ok<IEnumerable<PratoDTO>>>>
 (PratoDbContext pratoDbContext,
 IMapper mapper,
 string? pratoNome) =>
     {
         var pratos = mapper.Map<IEnumerable<PratoDTO>>(await pratoDbContext.Pratos.Where(x => pratoNome == null || x.Nome.ToLower().Contains(pratoNome.ToLower())).ToListAsync());

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

app.MapGet("/prato/{pratoId:int}", async
(PratoDbContext pratoDbContext,
IMapper mapper,
 int pratoId) =>
    {
        return mapper.Map<PratoDTO>(await pratoDbContext.Pratos.FirstOrDefaultAsync(x => x.Id == pratoId));
    }).WithName("GetPratoByPratoId");



app.MapPost("/prato", async Task<CreatedAtRoute<PratoDTO>>
 (
PratoDbContext pratoDbContext,
IMapper mapper,
PratoParaCriacaoDTO pratoParaCriacaoDTO
) =>
{
    var prato = mapper.Map<Prato>(pratoParaCriacaoDTO);
    pratoDbContext.Add(prato);
    await pratoDbContext.SaveChangesAsync();
    var pratoToReturn = mapper.Map<PratoDTO>(prato);

    return TypedResults.CreatedAtRoute(pratoToReturn, "GetPratoByPratoId", new { pratoId = prato.Id });
});

app.MapPut("/prato/{pratoId:int}", async Task<Results<NotFound, Ok>>
(PratoDbContext pratoDbContext,
IMapper mapper,
[FromBody] PratoParaAtualizacaoDTO pratoParaAtualizacaoDTO,
 int pratoId) =>
{

    var prato = await pratoDbContext.Pratos.FirstOrDefaultAsync(x => x.Id == pratoId);
    if (prato is null)
        return TypedResults.NotFound();
    

    mapper.Map(pratoParaAtualizacaoDTO, prato);

    await pratoDbContext.SaveChangesAsync();
    return TypedResults.Ok();
 });


 app.MapDelete("/prato/{pratoId:int}", async Task<Results<NotFound, NoContent>>
(PratoDbContext pratoDbContext,
IMapper mapper,
 int pratoId) =>
{

    var prato = await pratoDbContext.Pratos.FirstOrDefaultAsync(x => x.Id == pratoId);
    if (prato is null)
        return TypedResults.NotFound();
    
    pratoDbContext.Remove(prato);

    await pratoDbContext.SaveChangesAsync();
    return TypedResults.NoContent();
 });

app.Run();