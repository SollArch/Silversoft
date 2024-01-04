using Business.Constants;
using Business.Rules.Abstract;
using Core.Exceptions;
using DataAccess.Abstract;

namespace Business.Rules;

public class AdminPasswordRules : IAdminPasswordRules
{
    private readonly IAdminPasswordDal _adminPasswordDal;

    public AdminPasswordRules(IAdminPasswordDal adminPasswordDal)
    {
        _adminPasswordDal = adminPasswordDal;
    }

    public void CheckAnyPasswordExist()
    {
        if (_adminPasswordDal.GetAll().Count > 0) throw new BusinessException(Messages.AdminPassswordAlreadyExists);
    }
}