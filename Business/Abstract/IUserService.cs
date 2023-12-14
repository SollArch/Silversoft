using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(int userId);
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetByEmail(string email);
        IDataResult<User> GetByUserName(string userName);
        IDataResult<User> GetByStudentNumber(string studentNumber);
        IDataResult<User> GetById(int id);
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
        void AddAdmin();
    }
}