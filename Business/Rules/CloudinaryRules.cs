using Business.Rules.Abstract;
using CloudinaryDotNet.Actions;
using Core.Exceptions;

namespace Business.Rules;

public class CloudinaryRules : ICloudinaryRules
{
    public void CheckIfResultSucces(BaseResult result)
    {
        if (result.Error != null)
            throw new BusinessException(result.Error.Message);
    }
    
}