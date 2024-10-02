using MinimalApi.API.EndpointFilters;
using MinimalApi.API.EndpointHandlers;
using MinimalApi.API.Validations;

namespace MinimalApi.API.Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static void RegistrePratosEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("/rangos/{rangoId:int}", (int rangoId) => $"O prato {rangoId} é delicioso.")
            .WithOpenApi(operation =>
             { operation.Deprecated = true;
               return operation;
             })
             .WithSummary("Este endpoint está deprecated e será desacontinuado na v2 desta API")
             .WithDescription("Favor utilize a rota, /pratos/{pratosId} para evitar maiores transtornos futuros");


            var pratosEndpoints = endpointRouteBuilder.MapGroup("/pratos")
            .RequireAuthorization();
            var pratosComIdEndpoints = pratosEndpoints.MapGroup("/{pratoId:int}");
            
            var pratosComIdAndLockFilterEndpoints = endpointRouteBuilder.MapGroup("/pratos/{pratoId:int}")
            //Adcioando a politica de admin do Brasil, dizendo que somente esse perfil pode atualizar ou deletar pratos do banco.
            .RequireAuthorization("RequiredAdminFromBrazil")
            .RequireAuthorization()
            .AddEndpointFilter(new PratoIsLockedFilter(4))
            .AddEndpointFilter(new PratoIsLockedFilter(3));

            pratosEndpoints.MapGet("", PratosHandlers.GetPratosAsync);
            pratosComIdEndpoints.MapGet("", PratosHandlers.GetPratosByIdAsync).WithName("GetPratosById")
            .AllowAnonymous();
            pratosEndpoints.MapPost("", PratosHandlers.PostPratosAync)
            .AddEndpointFilter<ValidateAnotationFilter>();

            pratosComIdAndLockFilterEndpoints.MapPut("", PratosHandlers.PutPratosAsync);
            pratosComIdAndLockFilterEndpoints.MapDelete("", PratosHandlers.DeletePratosAsync)
            .AddEndpointFilter<LogNotFoundResponseFilter>();
        }
        public static void RegistreIngredientesEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {           
            var ingredientesEndpoints = endpointRouteBuilder.MapGroup("/pratos/{pratoId:int}/ingredientes")
            .RequireAuthorization();
            ingredientesEndpoints.MapGet("", IngredientesHandlers.GetIngredientesAsync);
        }
    }
}