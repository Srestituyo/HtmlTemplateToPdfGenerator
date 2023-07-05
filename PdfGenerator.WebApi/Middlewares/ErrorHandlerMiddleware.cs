using System.Net;
using System.Text.Json;
using PdfGenerator.Application.Common.Exceptions;
using PdfGenerator.Application.Common.Wrapper;

namespace PdfGenerator.WebApi.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate theNext)
    {
        _next = theNext;
    }

    public async Task Invoke(HttpContext theContext)
    {
        try
        {
            await _next(theContext);
        }
        catch (Exception error)
        {
            var aResponse = theContext.Response;
            aResponse.ContentType = "application/json";
            var aReponseModel = new Response<string>()
            {
                Succeeded = false,
                Message = error?.Message
            };

            switch (error)
            {
                case BadRequestException:
                case ApiException e:
                    //Custom Application error api
                    aResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    //Not  found error
                    aResponse.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case InvalidOperationException e:
                    //Object already Exists error
                    aResponse.StatusCode = (int)HttpStatusCode.Conflict;
                    break;
                default:
                    //unhandle error
                    aResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var aResult = JsonSerializer.Serialize(aReponseModel);
            await aResponse.WriteAsync(aResult);

        }
    }
}