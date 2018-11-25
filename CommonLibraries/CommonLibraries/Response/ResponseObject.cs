using System.Net;
using Newtonsoft.Json;

namespace CommonLibraries.Response
{
  public class ResponseObject
  {
    [JsonProperty("status")]
    public HttpStatusCode Status { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public object Data { get; set; }

    public ResponseObject()
    {
    }

    public ResponseObject(HttpStatusCode status, string message, object data)
    {
      Message = string.IsNullOrEmpty(message) ? GetDafaultMesage(status) : message;
      Status = status;
      Data = data;
    }

    public static string GetDafaultMesage(HttpStatusCode statusCode)
    {
      switch (statusCode)
      {
        case HttpStatusCode.OK: return "OK";
        case HttpStatusCode.Created: return "CREATED";
        case HttpStatusCode.NoContent: return "NO CONTENT";
        case HttpStatusCode.Accepted: return "UPDATED";
        case HttpStatusCode.NotModified: return "NOT MODIFIED";
        case HttpStatusCode.BadRequest: return "BAD REQUEST";
        case HttpStatusCode.Unauthorized: return "UNAUTHORIZED";
        case HttpStatusCode.Forbidden: return "FORBIDDEN";
        case HttpStatusCode.NotFound: return "NOT FOUND";
        case HttpStatusCode.Conflict: return "CONFLICT";
        case HttpStatusCode.InternalServerError: return "INTERNAL SERVER ERROR";
        default: return "DEFAULT INTERNAL SERVER ERROR";
      }
    }
  }
}