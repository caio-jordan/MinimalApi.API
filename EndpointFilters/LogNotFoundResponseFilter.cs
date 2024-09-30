using System.Net;

namespace MinimalApi.API.EndpointFilters
{
    public class LogNotFoundResponseFilter(ILogger<LogNotFoundResponseFilter> logger) : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            //LogInformartion with Endpoint Filter 
            var result = await next(context);

            var actualResults = (result is INestedHttpResult result1) ? result1.Result: (IResult)result;

            if (actualResults is IStatusCodeHttpResult {StatusCode: (int)HttpStatusCode.NotFound})
            logger.LogInformation($"Resource {context.HttpContext.Request.Path} was not found.");

            return result;
        }
    }
}