using System;
using System.Collections.Generic;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTO.Get;
using Entities.DTO.Post.User;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(Guid userId);
        IDataResult<UserGetDto> GetByEmailWithDto(string email);
        IDataResult<User> GetByEmail(string email);
        IDataResult<User> GetById(Guid userId);
        IDataResult<User> GetByUserName(string userName);
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(UserForDelete userForDelete);
        void AddAdmin();
    }
}