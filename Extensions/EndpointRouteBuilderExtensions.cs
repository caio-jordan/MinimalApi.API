using MinimalApi.API.EndpointFilters;
using MinimalApi.API.EndpointHandlers;
using MinimalApi.API.Validations;

namespace MinimalApi.API.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegistrePratosEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            var pratosEndpoints = endpointRouteBuilder.MapGroup("/pratos");
            var pratosComIdEndpoints = pratosEndpoints.MapGroup("/{pratosId:int}");

            var pratosComIdAndLockFilterEndpoints = endpointRouteBuilder.MapGroup("/pratos/{pratoId:int}")            
            .AddEndpointFilter(new PratoIsLockedFilter(4))
            .AddEndpointFilter(new PratoIsLockedFilter(3));

            pratosEndpoints.MapGet("", PratosHandlers.GetPratosAsync);
            pratosComIdEndpoints.MapGet("", PratosHandlers.GetPratosByIdAsync).WithName("GetPratos");
            pratosEndpoints.MapPost("", PratosHandlers.PostPratosAync)
            .AddEndpointFilter<ValidateAnotationFilter>();

            pratosComIdAndLockFilterEndpoints.MapPut("", PratosHandlers.PutPratosAsync);
            pratosComIdAndLockFilterEndpoints.MapDelete("", PratosHandlers.DeletePratosAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>();
        }
        public static void RegistreIngredientesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {           
            var ingredientesEndpoints = endpointRouteBuilder.MapGroup("/pratos/{pratoId:int}/ingredientes");
            ingredientesEndpoints.MapGet("", IngredientesHandlers.GetIngredientesAsync);
        }
    }
}