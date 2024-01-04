using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract;

public interface ICloudinaryService
{
    public IDataResult<string> Upload(IFormFile file,string name);
    public IResult Destroy(string publicId);
}