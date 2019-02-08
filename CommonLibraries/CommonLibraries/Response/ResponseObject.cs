using Newtonsoft.Json;

namespace CommonLibraries.Response
{
  public enum HttpStatusCode
  {
    OK = System.Net.HttpStatusCode.OK,
    Created = System.Net.HttpStatusCode.Created,
    NoContent = System.Net.HttpStatusCode.NoContent,
    Accepted = System.Net.HttpStatusCode.Accepted,
    NotModified = System.Net.HttpStatusCode.NotModified,
    BadRequest = System.Net.HttpStatusCode.BadRequest,
    Unauthorized = System.Net.HttpStatusCode.Unauthorized,
    Forbidden = System.Net.HttpStatusCode.Forbidden,
    NotFound = System.Net.HttpStatusCode.NotFound,
    Conflict = System.Net.HttpStatusCode.Conflict,
    InternalServerError = System.Net.HttpStatusCode.InternalServerError
  }

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

  public class ResponseObject<T> :  ResponseObject
  {

    [JsonProperty("data")]
    public new T Data { get; set; }

    public ResponseObject(HttpStatusCode status, string message, T data) : base(status, message, data)
    {
      Data = data;
    }
  }
}