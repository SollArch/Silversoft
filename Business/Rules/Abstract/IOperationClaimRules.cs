namespace Business.Rules.Abstract;

public interface IOperationClaimRules
{
    public void CheckIfNameExist(string operationClaimName);
}