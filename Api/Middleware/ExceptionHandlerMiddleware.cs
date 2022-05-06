using Application.Common.Models;
using Application.Exceptions;
using Common.DTOs;
using Newtonsoft.Json;
using System.Net;

namespace Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var message = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    message = JsonConvert.SerializeObject(validationException.ValidationErrors);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    message = badRequestException.Message;
                    break;
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case ForbiddenAccessException forbiddenAccessException:
                    httpStatusCode = HttpStatusCode.Forbidden;
                    break;
                case Exception ex:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (message == string.Empty)
            {
                message = exception.Message;
            }

            _logger.LogError(exception, message);

            var result = JsonConvert.SerializeObject(Result.Fail(message));
            return context.Response.WriteAsync(result);
        }
    }
}
