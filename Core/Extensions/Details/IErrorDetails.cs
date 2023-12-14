namespace Core.Extensions.Details;

public interface IErrorDetails
{
    int StatusCode { get; }
    string Message { get; }
    string GetDetails();
}