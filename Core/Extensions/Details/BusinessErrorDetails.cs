using System.Net;
using Newtonsoft.Json;

namespace Core.Extensions.Details;

public class BusinessErrorDetails : IErrorDetails
{
    public int StatusCode { get; } = (int)HttpStatusCode.BadRequest;
    public string Message { get; set; }
    public string GetDetails()
    {
        return JsonConvert.SerializeObject(this);
    }
}