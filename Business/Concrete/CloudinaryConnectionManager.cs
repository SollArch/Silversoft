using System;
using System.Linq;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Rules.Abstract;
using CloudinaryDotNet;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete;

public class CloudinaryConnectionManager : ICloudinaryConnectionService
{
    private readonly ICloudinaryConnectionDal _cloudinaryConnectionDal;
    private readonly ICloudinaryConnectionRules _cloudinaryConnectionRules;

    public CloudinaryConnectionManager(ICloudinaryConnectionDal cloudinaryConnectionDal, ICloudinaryConnectionRules cloudinaryConnectionRules)
    {
        _cloudinaryConnectionDal = cloudinaryConnectionDal;
        _cloudinaryConnectionRules = cloudinaryConnectionRules;
    }

    [SecuredOperation("admin")]
    public IResult AddCloudinarySettings(CloudinaryConnection cloudinaryConnection)
    {
        _cloudinaryConnectionRules.CheckIfCloudinaryConnectionExists();
        cloudinaryConnection.Id = Guid.NewGuid();
        _cloudinaryConnectionDal.Add(cloudinaryConnection);
        return new SuccessResult(Messages.CloudinarySettingsAdded);
    }
    
    [SecuredOperation("admin")]
    public IResult UpdateCloudinarySettings(CloudinaryConnection cloudinaryConnection)
    {
        _cloudinaryConnectionRules.CheckIfCloudinaryConnectionDoesNotExist(cloudinaryConnection.Id);
        _cloudinaryConnectionDal.Update(cloudinaryConnection);
        return new SuccessResult(Messages.CloudinarySettingsUpdated);
    }
    
    [SecuredOperation("admin")]
    public IResult DeleteCloudinarySettings(CloudinaryConnection cloudinaryConnection)
    {
        _cloudinaryConnectionRules.CheckIfCloudinaryConnectionDoesNotExist(cloudinaryConnection.Id);
        _cloudinaryConnectionDal.Delete(cloudinaryConnection);
        return new SuccessResult(Messages.CloudinarySettingsDeleted);
    }

    public Cloudinary GetCloudinary()
    {
        _cloudinaryConnectionRules.CheckIfCloudinaryConnectionNotExists();
        var result = _cloudinaryConnectionDal.GetAll().First();
        Console.WriteLine(result.Cloud);
        Console.WriteLine(result.ApiKey);
        Console.WriteLine(result.ApiSecret);
        var account = new Account
        {
            Cloud = result.Cloud,
            ApiKey = result.ApiKey,
            ApiSecret = result.ApiSecret
        };
        return new Cloudinary(account);
    }

}