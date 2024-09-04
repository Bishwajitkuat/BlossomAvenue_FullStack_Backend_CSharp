using BlossomAvenue.Service.CustomExceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlossomAvenue.Presentation.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            var (statusCode, message) = exception switch
            {
                RecordAlreadyExistsException or ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
                RecordNotCreatedException => (HttpStatusCode.NotFound, exception.Message),
                RecordNotFoundException => (HttpStatusCode.NotFound, exception.Message),
                RecordNotUpdatedException => (HttpStatusCode.NotModified, exception.Message),
                NotImplementedException => (HttpStatusCode.NotImplemented, exception.Message),
                _ => (HttpStatusCode.InternalServerError, $"Something unusual happened. Please try again or contact the system administrator\nError:{exception.Message}")
            };

            var response = new { error = message };

            var payload = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(payload);
        }
    }
}
