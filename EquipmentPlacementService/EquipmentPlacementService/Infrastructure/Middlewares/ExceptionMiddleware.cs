using EquipmentPlacementService.Infrastructure.Exceptions;
using EquipmentPlacementService.Infrastructure.Responses;
using System.Net;
using System.Text.Json;

namespace EquipmentPlacementService.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ErrorResponse();

            switch (ex)
            {
                case NotFoundException notFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = "NotFoundException occurred while processing your request.";
                    errorResponse.Error = notFoundException.Message;
                    break;

                case NotEnoughSpaceException notEnoughSpaceException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    errorResponse.Message = "NotEnoughSpaceException occurred while processing your request.";
                    errorResponse.Error = notEnoughSpaceException.Message;
                    break;

                default:
                    errorResponse.Message = "An error occurred while processing your request.";
                    errorResponse.Error = ex.Message;
                    break;
            }

            var json = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(json);
        }
    }
}

