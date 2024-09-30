namespace MinimalApi.API.EndpointFilters
{
    public class PratoIsLockedFilter : IEndpointFilter
    {
        public readonly int _lockedRangoId;

        public PratoIsLockedFilter(int lockedRangoId)
        {
            _lockedRangoId = lockedRangoId;
        }
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            int pratoId;

        switch (context.HttpContext.Request.Method)
        {
            case "PUT":
            pratoId  = context.GetArgument<int>(3);
            break;

            case "DELETE":
            pratoId  = context.GetArgument<int>(1);
            break;

            default:
            throw new NotSupportedException("this filter is not supported for this scenario.");
        }
        
        if(pratoId == _lockedRangoId)
        {
            return TypedResults.Problem(new(){
                Status = 400,
                Title = "Prato não pode ser alterado ou deletado",
                Detail = "Não é possivel modificar ou deletar o prato atual"
            });
        }
        var result = await next.Invoke(context);
        return result;            
        }
    }
}