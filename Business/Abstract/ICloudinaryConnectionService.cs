using CloudinaryDotNet;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface ICloudinaryConnectionService
{
    IResult AddCloudinarySettings(CloudinaryConnection cloudinaryConnection);
    IResult UpdateCloudinarySettings(CloudinaryConnection cloudinaryConnection);
    IResult DeleteCloudinarySettings(CloudinaryConnection cloudinaryConnection);
    Cloudinary GetCloudinary();

}