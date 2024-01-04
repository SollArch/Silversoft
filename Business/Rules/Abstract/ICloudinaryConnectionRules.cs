using System;

namespace Business.Rules.Abstract;

public interface ICloudinaryConnectionRules
{
    public void CheckIfCloudinaryConnectionExists();
    public void CheckIfCloudinaryConnectionNotExists();
    public void CheckIfCloudinaryConnectionDoesNotExist(Guid id);
}