using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CommonLibraries.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CommonLibraries.Response
{
  public class ResponseResult : IActionResult
  {
    protected readonly ResponseObject Response;

    public ResponseResult(HttpStatusCode status) : this(status, null, null)
    {
    }

    public ResponseResult(HttpStatusCode status, string message) : this(status, message, null)
    {
    }

    public ResponseResult(HttpStatusCode status, object data) : this(status, null, data)
    {
    }

    public ResponseResult(HttpStatusCode status, string message, object data)
    {
      Response = new ResponseObject(status, message, data);
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
      var serializerSettings =
        new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};
      context.HttpContext.Response.ContentType = "application/json";
      context.HttpContext.Response.StatusCode = (int)ChangeStatusCodeToSaveBody(Response.Status);
      await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(Response, serializerSettings));
    }

    private HttpStatusCode ChangeStatusCodeToSaveBody(HttpStatusCode httpStatusCode)
    {
      switch (httpStatusCode)
      {
        case HttpStatusCode.NoContent: return HttpStatusCode.OK;
        case HttpStatusCode.NotModified: return HttpStatusCode.OK;
        //case HttpStatusCode.BadRequest: return HttpStatusCode.OK;
        //case HttpStatusCode.NotModified:
        //  return HttpStatusCode.;
        default: return httpStatusCode;
      }
    }
  }

  public class OkResponseResult : ResponseResult
  {
    public OkResponseResult() : this(null, null)
    {
    }

    public OkResponseResult(string message) : this(message, null)
    {
    }

    public OkResponseResult(object data) : this(null, data)
    {
    }

    public OkResponseResult(string message, object data) : base(HttpStatusCode.OK, message, data)
    {
    }
  }

  public class UpdatedResponseResult : ResponseResult
  {
    public UpdatedResponseResult() : this(null, null)
    {
    }

    public UpdatedResponseResult(string message) : this(message, null)
    {
    }

    public UpdatedResponseResult(object data) : this(null, data)
    {
    }

    public UpdatedResponseResult(string message, object data) : base(HttpStatusCode.Accepted, message, data)
    {
    }
  }

  public class NoContentResponseResult : ResponseResult
  {
    public NoContentResponseResult() : this(null, null)
    {
    }

    public NoContentResponseResult(string message) : this(message, null)
    {
    }

    public NoContentResponseResult(object data) : this(null, data)
    {
    }

    public NoContentResponseResult(string message, object data) : base(HttpStatusCode.NoContent, message, data)
    {
    }
  }

  public class NotFoundResponseResult : ResponseResult
  {
    public NotFoundResponseResult() : this(null, null)
    {
    }

    public NotFoundResponseResult(string message) : this(message, null)
    {
    }

    public NotFoundResponseResult(object data) : this(null, data)
    {
    }

    public NotFoundResponseResult(string message, object data) : base(HttpStatusCode.NotFound, message, data)
    {
    }
  }

  public class BadResponseResult : ResponseResult
  {
    public BadResponseResult() : this(null, null)
    {
    }

    public BadResponseResult(string message) : this(message, null)
    {
    }

    public BadResponseResult(object data) : this(null, data)
    {
    }

    public BadResponseResult(ModelStateDictionary modelState) : base(HttpStatusCode.BadRequest, "Validation errors")
    {
      if (modelState.IsValid) throw new ArgumentException("ModelState must be invalid", nameof(modelState));

      var errors = modelState.Select(x => new {x.Key, Message = x.Value.Errors.Select(error=> error.ErrorMessage).ToList()}).ToList();
      Response.Data = new {Errors = errors};
    }

    public BadResponseResult(string message, object data) : base(HttpStatusCode.BadRequest, message, data)
    {
      if (message.IsNullOrEmpty() && data == null) Response.Message = "Input body is null.";
    }
  }
}