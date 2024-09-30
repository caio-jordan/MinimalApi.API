using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MinimalApi.API.Models;
using MiniValidation;

namespace MinimalApi.API.Validations
{
    public class ValidateAnotationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var pratoParaCriacaoDTO = context.GetArgument<PratoParaCriacaoDTO>(2);

            if (!MiniValidator.TryValidate(pratoParaCriacaoDTO, out var erros))
            return TypedResults.ValidationProblem(erros);

            return await next(context);
        }
    }
}