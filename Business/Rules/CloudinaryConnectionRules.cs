using System;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Exceptions;
using DataAccess.Abstract;

namespace Business.Rules;

public class CloudinaryConnectionRules : ICloudinaryConnectionRules
{
    private readonly ICloudinaryConnectionDal _cloudinaryConnectionDal;

    public CloudinaryConnectionRules(ICloudinaryConnectionDal cloudinaryConnectionDal)
    {
        _cloudinaryConnectionDal = cloudinaryConnectionDal;
    }

    public void CheckIfCloudinaryConnectionExists()
    {
        if (_cloudinaryConnectionDal.GetAll().Count > 0)
        {
            throw new BusinessException(Messages.CloudinarySettingsAlreadyExists);
        }
    }

    public void CheckIfCloudinaryConnectionNotExists()
    {
        if (_cloudinaryConnectionDal.GetAll().Count < 0)
        {
            throw new BusinessException(Messages.CloudinarySettingsAlreadyExists);
        }
    }

    public void CheckIfCloudinaryConnectionDoesNotExist(Guid id)
    {
        if(_cloudinaryConnectionDal.Get(x => x.Id == id) == null)
            throw new BusinessException(Messages.CloudinarySettingsDoesNotExist);
    }
}