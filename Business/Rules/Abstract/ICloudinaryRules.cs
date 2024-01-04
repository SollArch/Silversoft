using CloudinaryDotNet.Actions;

namespace Business.Rules.Abstract;

public interface ICloudinaryRules
{
    public void CheckIfResultSucces(BaseResult result);
}