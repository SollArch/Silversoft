using System.Collections.Generic;
using System.Net;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Core.Extensions.Details
{
    public class DefaultErrorDetails : IErrorDetails
    {
        public string Message { get; set; } 
        public string GetDetails()
        {
            return JsonConvert.SerializeObject(this);
        }

        public int StatusCode { get; set; } = (int)HttpStatusCode.InternalServerError;
        
    }
}
