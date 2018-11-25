using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommonLibraries.Exceptions.ApiExceptions;
using CommonLibraries.Response;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CommonLibraries.Exceptions
{
  public class ExceptionHandlingMiddleware
  {
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task Invoke(HttpContext context, IHostingEnvironment env,
      ILogger<ExceptionHandlingMiddleware> logger)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        logger.LogError(ex, ex.Message, "");
        await HandleExceptionAsync(context, ex);
        if (env.IsDevelopment()) Console.WriteLine(ex);
        throw;
      }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      if (context.Response.StatusCode == (int)HttpStatusCode.OK) context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      switch (exception)
      {
        case NotFoundException _:
          context.Response.StatusCode = (int)HttpStatusCode.NotFound;
          break;
      }
      var response = new ResponseObject((HttpStatusCode)context.Response.StatusCode, exception.Message, exception.StackTrace);

      var result = JsonConvert.SerializeObject(response);
      context.Response.ContentType = "application/json";
      return context.Response.WriteAsync(result);
    }
  }
}
