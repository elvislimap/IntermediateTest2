using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace IntermediateTest2.Service.Api.Handlers
{
    public static class HandlersExtensions
    {
        public static void DefaultExceptionHandler(this IApplicationBuilder app)
        {
            app.Run(async handler =>
            {
                var feature = handler.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;

                handler.Response.ContentType = "application/json";
                handler.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await handler.Response.WriteAsync(exception.Message);
            });
        }
    }
}