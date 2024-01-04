using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.Rules.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete;

public class AdminPasswordManager : IAdminPasswordService
{
    private readonly IAdminPasswordDal _adminPasswordDal;
    private readonly IAdminPasswordRules _rules;

    public AdminPasswordManager(IAdminPasswordDal adminPasswordDal, IAdminPasswordRules rules)
    {
        _adminPasswordDal = adminPasswordDal;
        _rules = rules;
    }
    
    [SecuredOperation("admin")]
    public IResult AddPassword(string password)
    {
        _rules.CheckAnyPasswordExist();
        var passwordToAdd = new AdminPassword
        {
            Id = Guid.NewGuid(),
            Password = password
        };
        _adminPasswordDal.Add(passwordToAdd);
        return new SuccessResult(Messages.AdminPasswordAdded);
    } 
    
    public string GetPassword()
    {
        var adminPassword = _adminPasswordDal.GetAll()[0];
        return adminPassword.Password;
    }
}