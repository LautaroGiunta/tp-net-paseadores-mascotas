using Microsoft.AspNetCore.Diagnostics;

namespace WebAPI
{
    // JSON mal formado o parámetro mal tipado: 400 con el motivo, sin stack trace
    public class ManejadorDeBadRequest : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            // Cualquier otra excepción sigue al 500 genérico
            if (exception is not BadHttpRequestException badRequest)
            {
                return false;
            }

            await Results.Json(new { error = badRequest.Message }, statusCode: badRequest.StatusCode)
                .ExecuteAsync(context);

            return true;
        }
    }
}