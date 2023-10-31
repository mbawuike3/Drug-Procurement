using Drug_Procurement.ExceptionHandlers;
using System.Net;

namespace Drug_Procurement.Middlewares;

public class ErrorHandlerMiddleWare
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleWare(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            Dictionary<string, string[]> errors = new()
            {
                {"Error", new string[] { error.Message} }
            };
            if(error.InnerException != null)
            {
                errors.Add("InnerException", new string[] { error.InnerException.Message});
            }
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Wrappers.Response<string>()
            {
                Succeeded = false,
                Message = "An error occurred",
                Errors = errors
            };
            string errorCode = "99";
            switch (error)
            {
                case ApiException e:
                    //general
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel = new Wrappers.Response<string>()
                    {
                        Succeeded = false,
                        Message = error.Message,
                        Code = errorCode,
                        Errors = errors
                    };
                    break;
                case ValidationException e:
                    //custom application error
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = e.Errors;
                    responseModel.Code = "10";
                    responseModel.Message = "Validation exception occurred";
                    break;
                case KeyNotFoundException e:
                    // Not Found Error
                    response.StatusCode= (int)HttpStatusCode.NotFound;
                    responseModel.Code = errorCode;
                    responseModel.Message = "KeyNotFound exception occurred";
                    break;
                default:
                    //unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.Code = errorCode;
                    break;
            }
            var result = System.Text.Json.JsonSerializer.Serialize(responseModel);
            await response.WriteAsync(result);
        }
    }
}
