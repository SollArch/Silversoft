using System;
using Business.Abstract;
using Business.Constants;
using Business.Rules.Abstract;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete;

public class CloudinaryManager : ICloudinaryService
{
    private readonly ICloudinaryConnectionService _cloudinaryConnectionService;
    private readonly ICloudinaryRules _cloudinaryRules;
    private readonly Cloudinary _cloudinary;

    public CloudinaryManager(ICloudinaryConnectionService cloudinaryConnectionService, ICloudinaryRules cloudinaryRules)
    {
        _cloudinaryConnectionService = cloudinaryConnectionService;
        _cloudinaryRules = cloudinaryRules;
        _cloudinary = GetCloudinary();
    }

    public IDataResult<string> Upload(IFormFile file,string name)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, file.OpenReadStream()),
            PublicId = Guid.NewGuid().ToString(),
            DisplayName = name
        };
        var uploadResult = _cloudinary.Upload(uploadParams);
        _cloudinaryRules.CheckIfResultSucces(uploadResult);
        
        return new SuccessDataResult<string>(data:uploadResult.PublicId,message:uploadResult.SecureUrl.AbsoluteUri);
    }

    public IResult Destroy(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = _cloudinary.Destroy(deleteParams);
        _cloudinaryRules.CheckIfResultSucces(result);
        return new SuccessResult(Messages.ImageDestroyedFromCloud);
    }

    private Cloudinary GetCloudinary()
    {
        return _cloudinaryConnectionService.GetCloudinary();
    }
}