using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MinimalApi.API.DbContexts;
using MinimalApi.API.Models;

namespace MinimalApi.API.EndpointHandlers
{
    public static class IngredientesHandlers
    {
        public static async Task<Results<NotFound, Ok<IEnumerable<IngredienteDTO>>>> GetIngredientesAsync
        (PratoDbContext pratoDbContext,
        IMapper mapper,
        int pratosId)
        {
            var prato = await pratoDbContext.Pratos.FirstOrDefaultAsync(x => x.Id == pratosId);
            if (prato is null)
                return TypedResults.NotFound();

            return TypedResults.Ok(mapper.Map<IEnumerable<IngredienteDTO>>((await pratoDbContext.Pratos
            .Include(prato => prato.Ingredientes)
            .FirstOrDefaultAsync(prato => prato.Id == pratosId))?.Ingredientes));
        }
    }
}