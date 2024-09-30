using Microsoft.AspNetCore.Routing;
using MinimalApi.API.EndpointHandlers;

namespace MinimalApi.API.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegistrePratosEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var pratosEndpoints = endpointRouteBuilder.MapGroup("/pratos");
            var pratosComIdEndpoints = pratosEndpoints.MapGroup("/{pratosId:int}");
            pratosEndpoints.MapGet("", PratosHandlers.GetPratosAsync);
            pratosComIdEndpoints.MapGet("", PratosHandlers.GetPratosByIdAsync).WithName("GetPratos");

            pratosEndpoints.MapPost("", PratosHandlers.PostPratosAync);

            pratosComIdEndpoints.MapPut("", PratosHandlers.PutPratosAsync);

            pratosComIdEndpoints.MapDelete("", PratosHandlers.DeletePratosAsync);


        }
        public static void RegistreIngredientesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {           
            var ingredientesEndpoints = endpointRouteBuilder.MapGroup("/pratos/{pratoId:int}/ingredientes");
            ingredientesEndpoints.MapGet("", IngredientesHandlers.GetIngredientesAsync);
        }
    }
}