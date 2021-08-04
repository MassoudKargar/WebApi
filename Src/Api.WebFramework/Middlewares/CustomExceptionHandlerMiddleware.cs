using System;
using System.Threading.Tasks;

using Api.Common;
using Api.Common.Exceptions;
using Api.WebFramework.Api;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

namespace Api.WebFramework.Middlewares
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
    public class CustomExceptionHandlerMiddleware
    {
        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
        {
            Next = next;
            Logger = logger;
        }

        public RequestDelegate Next { get; }
        public ILogger<CustomExceptionHandlerMiddleware> Logger { get; }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await Next(httpContext);
            }
            catch (AppException ex)
            {
                Logger.LogError(ex, ex.Message);

                ApiResult apiResult = new(false, ex.ApiStatusCode);
                var json = JsonConvert.SerializeObject(apiResult);
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(json);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "خطایی رخ داده");
                ApiResult apiResult = new(false, ApiResultStatusCode.ServerError);
                var json = JsonConvert.SerializeObject(apiResult);
                await httpContext.Response.WriteAsync(json);
            }
            finally
            {

            }
        }
    }
}
