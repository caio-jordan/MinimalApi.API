using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.API.DbContexts;
using MinimalApi.API.Entities;
using MinimalApi.API.Models;

namespace MinimalApi.API.EndpointHandlers
{
    public static class PratosHandlers
    {
        public static async Task<Results<NoContent, Ok<IEnumerable<PratoDTO>>>> GetPratosAsync
        (PratoDbContext pratoDbContext,
        IMapper mapper,
        [FromQuery(Name = "nome")] string? pratoNome)
        {
            var pratos = mapper.Map<IEnumerable<PratoDTO>>(await pratoDbContext.Pratos
                                                                .Where(x => pratoNome == null || x.Nome.ToLower().Contains(pratoNome.ToLower()))
                                                                .ToListAsync());

            if (!pratos.Any())
            {
                return TypedResults.NoContent();
            }
            return TypedResults.Ok(pratos);
        }

        public static async Task<PratoDTO> GetPratosByIdAsync
        (PratoDbContext pratoDbContext,
        IMapper mapper,
        int pratoId)
        {
            return mapper.Map<PratoDTO>(await pratoDbContext.Pratos.FirstOrDefaultAsync(x => x.Id == pratoId));
        }

        public static async Task<CreatedAtRoute<PratoDTO>> PostPratosAync
         (
        PratoDbContext pratoDbContext,
        IMapper mapper,
        PratoParaCriacaoDTO pratoParaCriacaoDTO
        )
        {
            var prato = mapper.Map<Prato>(pratoParaCriacaoDTO);
            pratoDbContext.Add(prato);
            await pratoDbContext.SaveChangesAsync();
            var pratoToReturn = mapper.Map<PratoDTO>(prato);

            return TypedResults.CreatedAtRoute(pratoToReturn, "GetPratos", new { pratoId = prato.Id });
        }

        public static async Task<Results<NotFound, Ok>> PutPratosAsync
        (PratoDbContext pratoDbContext,
        IMapper mapper,
        [FromBody] PratoParaAtualizacaoDTO pratoParaAtualizacaoDTO,
        int pratoId)
        {

            var prato = await pratoDbContext.Pratos.FirstOrDefaultAsync(x => x.Id == pratoId);
            if (prato is null)
                return TypedResults.NotFound();


            mapper.Map(pratoParaAtualizacaoDTO, prato);

            await pratoDbContext.SaveChangesAsync();
            return TypedResults.Ok();
        }

        public static async Task<Results<NotFound, NoContent>> DeletePratosAsync
        (PratoDbContext pratoDbContext,
        int pratoId)
        {

            var prato = await pratoDbContext.Pratos.FirstOrDefaultAsync(x => x.Id == pratoId);
            if (prato is null)
                return TypedResults.NotFound();

            pratoDbContext.Remove(prato);

            await pratoDbContext.SaveChangesAsync();
            return TypedResults.NoContent();
        }

    }


}