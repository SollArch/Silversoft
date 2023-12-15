using Core.DataAccess;
using DataAccess.Context;
using Entities.Concrete;

namespace DataAccess.Abstract;

public interface IPasswordDal : IEntityRepository<AdminPassword>
{
    
}