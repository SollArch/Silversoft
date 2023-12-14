using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.Extensions.Details;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using static Core.Extensions.Details.DefaultErrorDetails;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private IEnumerable<ValidationFailure> _errors;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";

            IErrorDetails errorDetails = e switch
            {
                ValidationException validationException => new ValidationErrorDetails
                {
                    Message = validationException.Message, ValidationErrors = validationException.Errors
                },
                BusinessException businessException => new BusinessErrorDetails
                {
                    Message = businessException.Message
                },
                _ => new DefaultErrorDetails()
                {
                    Message = e.Message
                }
            };

            httpContext.Response.StatusCode = errorDetails.StatusCode;

            return httpContext.Response.WriteAsync(errorDetails.GetDetails());
        }
    }
}