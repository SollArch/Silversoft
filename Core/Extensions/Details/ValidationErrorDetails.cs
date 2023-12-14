using System.Collections.Generic;
using System.Net;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Core.Extensions.Details;

public class ValidationErrorDetails : IErrorDetails
{
    public IEnumerable<ValidationFailure> ValidationErrors { get; set; }
    public int StatusCode { get; } = (int)HttpStatusCode.BadRequest;
    public string Message { get; set; }

    public string GetDetails()
    {
        return JsonConvert.SerializeObject(this);
    }
}