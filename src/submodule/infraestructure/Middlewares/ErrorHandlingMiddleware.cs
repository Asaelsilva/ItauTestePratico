using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Itau.Infrastructure.Middlewares
{
  public class ErrorHandlingMiddleware
  {
    private static readonly JsonSerializerSettings _defaultJsonSerializerSettings = new()
    {
      ContractResolver = new CamelCasePropertyNamesContractResolver(),
      NullValueHandling = NullValueHandling.Ignore,
    };

    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
        _logger.LogError(ex, "{Message}", ex.Message);

        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex.Message);
        Console.ResetColor();

        await HandleExceptionAsync(context, ex);
      }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      Console.WriteLine("############################");
      Console.WriteLine(exception.Message);
      Console.WriteLine("############################");
      HttpStatusCode code = HttpStatusCode.InternalServerError;
      IDictionary<string, string[]> fluentValidationErrors = null;

      if (exception is Exceptions.BusinessException)
      {
        code = HttpStatusCode.BadRequest;
        fluentValidationErrors = (exception as Exceptions.BusinessException)?.FluentValidationResult?.ToDictionary();
      }

      var errorMessage = (exception.InnerException != null) ? exception.InnerException.Message : exception.Message;
      var result = JsonConvert.SerializeObject(new Common.BaseController.Return<string>
      {
        Message = (code == HttpStatusCode.InternalServerError || fluentValidationErrors != null) ? null : errorMessage,
        Exception = (code == HttpStatusCode.InternalServerError) ? exception : null,
        Errors = fluentValidationErrors
      }, _defaultJsonSerializerSettings);

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)code;

      return context.Response.WriteAsync(result);
    }
  }
}
