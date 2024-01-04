using System;
using System.Collections.Generic;
using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.DTO.Get;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(Guid userId);
        UserGetDto GetUserDtoByEmail(string email);
    } 
}